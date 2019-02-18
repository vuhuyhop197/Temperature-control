
/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "stm32f4xx_hal.h"
#include "stdio.h"
#include "math.h"
#include "pid.h"
#include "nokia5110_LCD.h"
#include "stdlib.h"
#include "string.h"
#include "stm32f4xx_it.h"

/* Private variables ---------------------------------------------------------*/
//#define Set_temp	60
UART_HandleTypeDef huart3;
ADC_HandleTypeDef hadc1;
TIM_HandleTypeDef htim2;
#define ERR_MESSAGE__NO_MEM "Not enough memory!"
#define allocator(element, type) _allocator(element, sizeof(type))
	

uint8_t Num1,Num2;
uint8_t data_length;
volatile float temp;
volatile uint8_t counter=0,i=0;
volatile uint8_t x=0,intMode=0;
float value=0,error;
uint8_t status,Enable=0,Temperatue,uart,Deadband=0;
float Pid_val,val=0;
char buffer[8],output[20];
char *data_tr;
uint8_t recieve_data[20];
PID_PARAMETERS para;
char cmd[50];

void OnOff_Ctl(uint8_t t,uint8_t DB);
void Dimer_Led(uint8_t duty);
uint8_t temp_filter(float tem);
void Decode_Command(uint8_t *str);
void SystemClock_Config(void);
void uart_puts(char *chptr);
void uart_putc(uint8_t c);
void uart_gets(char *s);
char *append(const char *input, const char c);
static void MX_GPIO_Init(void);
static void MX_ADC1_Init(void);
static void MX_USART3_UART_Init(void);
static void MX_TIM2_Init(void);
static void *_allocator(size_t element, size_t typeSize);
/**
  * @brief  The application entry point.
  * Temperature control (25-80oC)
  * @retval None
  */
int main(void)
{
	
  HAL_Init();
	para.PID_Saturation=100;
	para.Ts=0.1;
  SystemClock_Config();
	MX_GPIO_Init();
  MX_ADC1_Init();
  MX_TIM2_Init();
	MX_USART3_UART_Init();
	
	LCD_setRST(GPIOC,GPIO_PIN_10);
	LCD_setCE(GPIOA,GPIO_PIN_2);
	LCD_setCLK(GPIOA,GPIO_PIN_8);
	LCD_setDC(GPIOA,GPIO_PIN_10);
	LCD_setDIN(GPIOA,GPIO_PIN_4);
	LCD_setLight(GPIOA,GPIO_PIN_6);
	LCD_init();
	LCD_invert(false);
  /* USER CODE BEGIN 2 */
	HAL_TIM_Base_Start_IT(&htim2);
	pid_reset_parmas(&para);
	pid_set_k_parmas(&para,-4,0,-0.001);
	HAL_UART_Receive_IT(&huart3,&recieve_data[0],2);
  while (1)
	{
		
		LCD_print("Temperature:",1,0);
		LCD_print(output,3,1);
		LCD_print("Set Temp:",0,2);
		LCD_print(buffer,60,2);
		if(intMode==0){
			LCD_print("PID control",1,4);
		}
		else{
			LCD_print("ON/OFF control",1,4);
		}
		/*Pid set parametrer*/

		if ((counter%5)==0){
		Pid_val = pid_process(&para,get_error(value,Temperatue));
		}
		if (intMode==0){
			Dimer_Led(Pid_val);			//light control each 1s
		}
		else{
			OnOff_Ctl(Temperatue,Deadband);
		}
		ftoa(Temperatue,buffer,2);
		if (counter==49){
			ftoa(value,output,3);	//convert float to string
			uart_puts(output);
			uart_putc('\n');
		}
		
		status = temp_filter(temp);
		if(status==63){
		value = val/64;				//mean filter 64 value
		}
		HAL_ADC_Start_IT(&hadc1);
  }
}
void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart){
	if (huart->Instance== huart3.Instance){	
		if (i==0){
			data_length = atoi((char *)recieve_data);
			memset(&recieve_data[0], 0, sizeof(recieve_data));
			i++;		
		}
		else{
			Decode_Command(recieve_data);
			data_length = 2;
			i=0;
		}
		HAL_UART_Receive_IT(&huart3,&recieve_data[0],data_length);
	}
}
		/*-------------------------*/
		/*-------------------------*/
/*Timer ISR ---------------------------------------------------------*/
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
	if (htim->Instance==htim2.Instance)
	{
		counter++;
		if (counter > 50){
		counter =0;				//reset counter		
		}
	}
}
/*ADC12B ISR ---------------------------------------------------------*/
void HAL_ADC_ConvCpltCallback(ADC_HandleTypeDef* hadc)
{
	if(hadc->Instance == hadc1.Instance)
	{
		temp = (((float)HAL_ADC_GetValue(hadc)/4095)*300); //convert ADC to temp
		HAL_GPIO_TogglePin(GPIOD,GPIO_PIN_3);
				x = 1;  //enable ADC
	}
}
void uart_putc(uint8_t c){
	HAL_UART_Transmit(&huart3,&c,1,50); //transfer a char
}
void uart_puts(char *chptr){
	while(*chptr != '\0'){
		uart_putc(*chptr);
		chptr++;
	}
}
/*Mean Calculate ---------------------------------------------------------*/
uint8_t temp_filter (float tem){	
	static uint8_t i=0;
if(x==1){
	val += tem; 
	i++;
	x=0;
}
if (i==64){
	i =0;
	val=tem;					//restart loop
}
	return i;
}
/*Period Control ---------------------------------------------------------*/
void Dimer_Led(uint8_t duty){
	if (counter <(duty*50/100)){
	HAL_GPIO_WritePin(GPIOD,GPIO_PIN_1,GPIO_PIN_SET);
	}
	else 
	{
		HAL_GPIO_WritePin(GPIOD,GPIO_PIN_1,GPIO_PIN_RESET);
	}
}
/*On/Off Control ---------------------------------------------------------*/
void OnOff_Ctl(uint8_t t,uint8_t DB){
	if (value<(t - DB)){
	Dimer_Led(100);
	}
	else if(value > (t + DB)){
	Dimer_Led(0);
	}
}
void Decode_Command(uint8_t *str){
	char Kp[2]={'\0'}, Ki[6]={'\0'}, Kd[6]={'\0'},Set_point[5]={'\0'},DB[2]={'\0'};
	uint8_t i=0,tem=0,j=0;
	while(recieve_data[j]!='\0'){
		if(recieve_data[j] == '#'){
			tem++;
			i=0;
			j++;
		}
		else if(recieve_data[j]=='P'){
			intMode = 0;
		}
		else if(recieve_data[j]=='O'){
			intMode = 1;
		}
		switch(tem){
			case 1:
					Kp[i] = recieve_data[j];
					i++;
			break;
			case 2:
				Ki[i] = recieve_data[j];
				i++;
			break;
			case 3:
				Kd[i] = recieve_data[j];
				i++;
			break;
			case 4:
				Set_point[i] = recieve_data[j];
				i++;
			break;
			case 5:
				DB[i] = recieve_data[j];
				i++;
			break;
			default: break;
		}
		j++;
	}
	pid_set_k_parmas(&para,atof(Kp),atof(Kd),atof(Ki));
	Temperatue = atoi(Set_point);
	Deadband = atoi(DB);
	tem=0;
	memset(&recieve_data[0], 0, sizeof(recieve_data));
}
void SystemClock_Config(void)
{

  RCC_OscInitTypeDef RCC_OscInitStruct;
  RCC_ClkInitTypeDef RCC_ClkInitStruct;

    /**Configure the main internal regulator output voltage 
    */
  __HAL_RCC_PWR_CLK_ENABLE();

  __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);

    /**Initializes the CPU, AHB and APB busses clocks 
    */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLM = 8;
  RCC_OscInitStruct.PLL.PLLN = 336;
  RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;
  RCC_OscInitStruct.PLL.PLLQ = 4;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

    /**Initializes the CPU, AHB and APB busses clocks 
    */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV4;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV4;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_5) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

    /**Configure the Systick interrupt time 
    */
  HAL_SYSTICK_Config(HAL_RCC_GetHCLKFreq()/1000);

    /**Configure the Systick 
    */
  HAL_SYSTICK_CLKSourceConfig(SYSTICK_CLKSOURCE_HCLK);

  /* SysTick_IRQn interrupt configuration */
  HAL_NVIC_SetPriority(SysTick_IRQn, 0, 0);
}

/* ADC1 init function */
static void MX_ADC1_Init(void)
{

  ADC_ChannelConfTypeDef sConfig;

    /**Configure the global features of the ADC (Clock, Resolution, Data Alignment and number of conversion) 
    */
  hadc1.Instance = ADC1;
  hadc1.Init.ClockPrescaler = ADC_CLOCK_SYNC_PCLK_DIV2;
  hadc1.Init.Resolution = ADC_RESOLUTION_12B;
  hadc1.Init.ScanConvMode = DISABLE;
  hadc1.Init.ContinuousConvMode = DISABLE;
  hadc1.Init.DiscontinuousConvMode = DISABLE;
  hadc1.Init.ExternalTrigConvEdge = ADC_EXTERNALTRIGCONVEDGE_NONE;
  hadc1.Init.ExternalTrigConv = ADC_SOFTWARE_START;
  hadc1.Init.DataAlign = ADC_DATAALIGN_RIGHT;
  hadc1.Init.NbrOfConversion = 1;
  hadc1.Init.DMAContinuousRequests = DISABLE;
  hadc1.Init.EOCSelection = ADC_EOC_SINGLE_CONV;
  if (HAL_ADC_Init(&hadc1) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

    /**Configure for the selected ADC regular channel its corresponding rank in the sequencer and its sample time. 
    */
  sConfig.Channel = ADC_CHANNEL_0;
  sConfig.Rank = 1;
  sConfig.SamplingTime = ADC_SAMPLETIME_3CYCLES;
  if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* TIM2 init function */
static void MX_TIM2_Init(void)
{

  TIM_ClockConfigTypeDef sClockSourceConfig;
  TIM_MasterConfigTypeDef sMasterConfig;

  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 42000;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 39;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/* USART3 init function */
static void MX_USART3_UART_Init(void)
{

  huart3.Instance = USART3;
  huart3.Init.BaudRate = 9600;
  huart3.Init.WordLength = UART_WORDLENGTH_8B;
  huart3.Init.StopBits = UART_STOPBITS_1;
  huart3.Init.Parity = UART_PARITY_NONE;
  huart3.Init.Mode = UART_MODE_TX_RX;
  huart3.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart3.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart3) != HAL_OK)
  {
    _Error_Handler(__FILE__, __LINE__);
  }

}

/** Configure pins as 
        * Analog 
        * Input 
        * Output
        * EVENT_OUT
        * EXTI
*/
static void MX_GPIO_Init(void)
{

  GPIO_InitTypeDef GPIO_InitStruct;

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOH_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();
  __HAL_RCC_GPIOD_CLK_ENABLE();
  __HAL_RCC_GPIOC_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOA, GPIO_PIN_2|GPIO_PIN_4|GPIO_PIN_6|GPIO_PIN_8 
                          |GPIO_PIN_10, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOC, GPIO_PIN_10, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOD, GPIO_PIN_1|GPIO_PIN_3|GPIO_PIN_5, GPIO_PIN_RESET);

  /*Configure GPIO pins : PA2 PA4 PA6 PA8 
                           PA10 */
  GPIO_InitStruct.Pin = GPIO_PIN_2|GPIO_PIN_4|GPIO_PIN_6|GPIO_PIN_8 
                          |GPIO_PIN_10;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

  /*Configure GPIO pin : PD11 */
  GPIO_InitStruct.Pin = GPIO_PIN_11;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_RISING;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(GPIOD, &GPIO_InitStruct);

  /*Configure GPIO pins : PD12 PD14 */
  GPIO_InitStruct.Pin = GPIO_PIN_12|GPIO_PIN_14;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_FALLING;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(GPIOD, &GPIO_InitStruct);

  /*Configure GPIO pin : PC10 */
  GPIO_InitStruct.Pin = GPIO_PIN_10;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH;
  HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

  /*Configure GPIO pins : PD1 PD3 PD5 */
  GPIO_InitStruct.Pin = GPIO_PIN_1|GPIO_PIN_3|GPIO_PIN_5;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH;
  HAL_GPIO_Init(GPIOD, &GPIO_InitStruct);

  /* EXTI interrupt init*/
  HAL_NVIC_SetPriority(EXTI15_10_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI15_10_IRQn);

}

/* USER CODE BEGIN 4 */

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @param  file: The file name as string.
  * @param  line: The line in file as a number.
  * @retval None
  */
void _Error_Handler(char *file, int line)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  while(1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t* file, uint32_t line)
{ 
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

