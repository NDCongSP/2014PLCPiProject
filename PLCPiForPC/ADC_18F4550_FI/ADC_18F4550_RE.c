#include <18f4550.h>
#device *=16 ADC=10
#include <stdlib.h>
#include <string.h>
#include <math.h>
#FUSES HS,NOWDT,CPUDIV1,NOLVP  
#use delay(clock=20000000)
#use rs232(baud=9600,parity=N,xmit=PIN_C6,rcv=PIN_C7,bits=8)
char ok=0;
char empty[]="";
char s1[] = "ABC-ABC-ABC#";
void ngung_ADC();
void chay_ADC(int16 i);
void main()
{
  
      int16 i,j;
      int32 analog = 0;      
      char s[117]="ABC-ABC-ABC";
      char str_temp[6],stt[3],ai[]="I",Xchar[]=":";
      setup_adc_ports(ALL_ANALOG|VSS_VDD);
      setup_ADC(ADC_CLOCK_INTERNAL);
      enable_interrupts(GLOBAL);
      delay_ms(10);
    while(1)
   {

            for(i=0;i<=12;i++)
               {
                  itoa(i,10,stt);
                  strcat(s,ai); 
                  strcat(s,stt); 
                  strcat(s,Xchar);
                  delay_us(100);
                  chay_ADC(i);
                  delay_us(100);
                  for(j=0; j<1500; j++)
                  {
                     analog = analog + (int32)read_adc();
                  }
                  analog = analog / 1500;
                  //output_high(pin_D0)   ;
                  if(i>4 &&analog<204)
                     analog = 0;
                  
                  if(i<5)
                  {
                     switch(i)
                     {
                        case 0: if(analog<10) output_low(pin_D4);
                              else   output_high(pin_D4)   ;   
                           break;
                        case 1:if(analog<10) output_low(pin_D3);
                              else   output_high(pin_D3);
                           break;
                        case 2:if(analog<10) output_low(pin_D2);
                              else   output_high(pin_D2);
                           break;
                        case 3:if(analog<10) output_low(pin_D1);
                              else   output_high(pin_D1);
                           break;
                        case 4:if(analog<10) output_low(pin_D0);
                              else   output_high(pin_D0);
                           break;
                     }
                  }   
                 
                  itoa(analog,10,str_temp);
                  strcat(s,str_temp);
                  strcpy(str_temp,empty);
                  analog=0;
               }
                  strcat(s,s1);//chuoi gui len PLCPi cos dang "ABC-ABC-ABCI0:0I1:0I2:0I3:0I4:0I5:0I6:0I7:0I8:0I9:0I10:0I11:0I12:0ABC-ABC-ABC#"
                  printf("%s\r\n",&s);
                  strcpy(s,empty);
                  s = "ABC-ABC-ABC";
                  delay_ms(10);
                  
           
   }   
}   
void ngung_ADC()
{
   setup_ADC(ADC_OFF);
}
void chay_ADC(int16 i)
{
      set_ADC_channel(i);
      delay_us(100);
}

