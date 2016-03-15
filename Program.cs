using System;  
using MonoBrick.EV3;//use this to run the example on the EV3
//using MonoBrick.NXT;//use this to run the example on the NXT  
namespace Application  
{  
    public static class Program{

      static void Main(string[] args)  
      {
          try
          {
              var brick = new Brick<ColorSensor, TouchSensor, Sensor, UltrasonicSensor>("COM4");
              brick.Connection.Close();
              brick.Connection.Open();
              sbyte speed = 0;
              sbyte speed2 = 0;
              brick.Sensor1.Mode = ColorMode.Color;
              brick.Sensor2.Mode = TouchMode.Boolean;
              brick.Sensor4.Mode = UltrasonicMode.Centimeter;
              int color = brick.Sensor1.Read();
              float distance = brick.Sensor4.Read();
              int startTouch = brick.Sensor2.Read();
              Console.WriteLine("Press Q to quit");



              int flag = 1;
              while (flag == 1)
              {
                  startTouch = brick.Sensor2.Read();
                  if (startTouch == 1)
                  {
                      speed = 0;

                      Console.WriteLine(speed.ToString());

                      if (speed < 100)
                          speed = (sbyte)(speed + 50);

                      brick.MotorA.On(speed);
                      brick.MotorD.On(speed);
                      // Very Important!
                      color = brick.Sensor1.Read();

                      Console.WriteLine("Initial Value: " + color.ToString());

                      //start code
                      while (color != 6)
                      {
                          Console.WriteLine("Current Color: " + color.ToString());
                          color = brick.Sensor1.Read();
                      }

                      if (speed > -100)
                          speed = (sbyte)(speed - 100);
                      brick.MotorA.On(speed);
                      brick.MotorD.On(speed);

                      System.Threading.Thread.Sleep(670);

                      Console.WriteLine("Speed before loop: " + speed);
                      Console.WriteLine("Speed2 before loop: " + speed2);

                      for (int count = 0; count < 100; count--)
                      {

                          speed = (sbyte)(speed + 100);
                          speed2 = (sbyte)(speed2 - 50);

                          brick.MotorA.On(speed);
                          brick.MotorD.On(speed2);

                          System.Threading.Thread.Sleep(300);

                          distance = brick.Sensor4.Read();

                          Console.WriteLine("Initial distance: " + distance);
                          while (distance > 40)
                          {
                              distance = brick.Sensor4.Read();
                              startTouch = brick.Sensor2.Read();
                              if (startTouch == 1)
                              {
                                  count = 101;
                                  break;
                              }

            
                          }

                          speed = (sbyte)(speed + 50);
                          brick.MotorA.On(speed);
                          brick.MotorD.On(speed);

                          color = brick.Sensor1.Read();

                          while (color != 6)
                          {
                              color = brick.Sensor1.Read();
                              startTouch = brick.Sensor2.Read();

                              if (startTouch == 1)
                              {
                                  count = 101;
                                  break;
                              }
                          }


                          Console.WriteLine("Speed when sees something: " + speed);

                          speed = (sbyte)(speed - 200);
                          brick.MotorA.On(speed);
                          brick.MotorD.On(speed);

                          System.Threading.Thread.Sleep(800);

                          speed = -50;
                          speed2 = 0;

                      }
                      brick.MotorA.Off();
                      brick.MotorD.Off();

                      Console.WriteLine(speed.ToString());
                      Console.WriteLine(speed2.ToString());
                  }

                  if (startTouch == 0)
                  {

                      brick.MotorA.Off();
                      brick.MotorD.Off();
                  }

              }
          }


          catch (Exception e)
          {
              Console.WriteLine("Error: " + e.Message);
              Console.WriteLine("Press any key to end...");
              Console.ReadKey();
          }  
      }  
    }
  
}

/* while (distance > 50)
                          {
                              brick.MotorA.ResetTacho();
                              int tachoA = brick.MotorA.GetTachoCount();


                              brick.MotorA.On(speed);
                              brick.MotorD.On(speed2);

                              while (tachoA > -300 || distance > 50)
                              {
                                  tachoA = brick.MotorA.GetTachoCount();
                                  Console.WriteLine(tachoA);
                                  distance = brick.Sensor4.Read();
                                  Console.WriteLine("Distance value: " + distance);
                                  if (tachoA > -300) break;
                              }

                              distance = brick.Sensor4.Read();

                              brick.MotorA.On(speed2);
                              brick.MotorD.On(speed);
                              if (distance < 50)
                              {
                                  tachoA = 300;
                              }
                              else
                              {
                                  brick.MotorA.ResetTacho();
                                  tachoA = brick.MotorA.GetTachoCount();
                              }

                              while (tachoA < 300 || distance > 50)
                              {
                                  tachoA = brick.MotorA.GetTachoCount();
                                  Console.WriteLine(tachoA);
                                  distance = brick.Sensor4.Read();
                                  Console.WriteLine("Distance value: " + distance);
                                  if (tachoA <= 300) break;
                              }

                              distance = brick.Sensor4.Read();

                          }*/