
using System;

namespace Internship_3_OOP
{
    public class Call
    {
        public DateTime CallTime { get; set; }
        public string CallState { get; set; }

        public Call() 
        {
            CallTime=DateTime.Now;
            CallState="zavrsen";
        }


        public void newCall()
        {
            CallTime=DateTime.Now;
            var randomNum = new Random();
            int randomState =randomNum.Next(3);

            switch (randomState)
            {
                case 0:
                    CallState = "u tijeku";
                    break;
                case 1:
                    CallState = "propusten";
                    break;
                case 2:
                    CallState = "zavrsen";
                    break;
            }
        }

        public void callSet(DateTime time, string state)
        {
            CallTime = time;
            CallState = state;
        }
    }
}
