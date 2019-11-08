using System;
using System.Threading;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FibUhr.ViewModels
{
    /*
     * Kombinationen
     *  1 = {(1);(1*)}
     *  2 = {(2);(1,1*)}
     *  3 = {(3);(2,1);(2,1*)}
     *  4 = {(1,1*,2);(3,1);(3,1*)}
     *  5 = {(1,1*,3),(2,3),(5)}
     *  6 = {(5,1)(5,1*);(3,2,1),(3,2,1*)}
     *  7 = {(5,2);(5,1,1*)}
     *  8 = {(5,3);(5,2,1);(5,2,1*)}
     *  9 = {(5,3,1);(5,3,1*);(5,2,1,1*)}
     * 10 = {(5,3,2);(5,3,1,1*)}
     * 11 = {(5,3,2,1),(5,3,2,1*)}
     */
    public enum Color {
        White,  // reset
        Red,    // hour
        Green,  // minute 
        Blue    // hour & minute 
    };

    class FibonacciClockViewModel : INotifyPropertyChanged
    {
        private string five = "white";
        private string three = "white";
        private string two = "white";
        private string oneO = "white";
        private string oneU = "white";
        private int ticks = 0;
        public int Hour {
            get => DateTime.Now.Hour % 12;
            //get => 6;
        }
        public int Minute {
            get => DateTime.Now.Minute / 5;
            //get => 30 / 5;
        }
        public string OneO
        {
            get => oneO;

            set
            {
                oneO = value;
                OnNotifyPropertyChanged("OneO");
            }
        }
        public string OneU
        {
            get => oneU;

            set
            {
                oneU = value;
                OnNotifyPropertyChanged("OneU");
            }
        }
        public string Two
        {
            get => two;

            set
            {
                two = value;
                OnNotifyPropertyChanged("Two");
            }
        }
        public string Three
        {
            get => three;

            set
            {
                three = value;
                OnNotifyPropertyChanged("Three");
            }
        }
        public string Five
        {
            get => five;

            set
            {
                five = value;
                OnNotifyPropertyChanged("Five");
            }
        }

        public FibonacciClockViewModel()
        {
            InitAsync();
        }

        private async Task InitAsync()
        {
            RestTimeColor();
            SetFibonacciClock();
            await ExecuteStartFibonacciClockAsync();
        }

        public void SetFibonacciClock()
        {
            RestTimeColor();
            SetHour();
            SetMinute();
        }

        private async Task ExecuteStartFibonacciClockAsync()
        {

            await Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    // call the UI to update your bound collection
                    SetFibonacciClock();

                    // give the ui some time to respond before continue with your endless-loop
                    Thread.Sleep(200);
                }
            });
        }

        private void RestTimeColor()
        {
            Five = Color.White.ToString();
            Three = Color.White.ToString();
            Two = Color.White.ToString();
            OneO = Color.White.ToString();
            OneU = Color.White.ToString();
        }

        private void SetHour()
        {
            setTicks();
            SetTime(Hour, Color.Red.ToString());
        }

        private void SetMinute()
        {

            setTicks();
            SetTime(Minute, Color.Green.ToString());
        }
        private void setTicks()
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                this.ticks = BitConverter.ToInt32(rno, 0);
            }
        }

        private void SetTime(int rest = 0, string useColor = "White")
        {
            switch (rest)
            {
                case 1:
                    SetOne(useColor);
                    break;
                case 2:
                    SetTwo(useColor);
                    break;
                case 3:
                    SetThree(useColor);
                    break;
                case 4:
                    SetFour(useColor);
                    break;
                case 5:
                    SetFive(useColor);
                    break;
                case 6:
                    SetSix(useColor);
                    break;
                case 7:
                    SetSeven( useColor);
                    break;
                case 8:
                    SetEight( useColor);
                    break;
                case 9:
                    SetNine(useColor);
                    break;
                case 10:
                    SetTen(useColor);
                    break;
                case 11:
                    SetEleven(useColor);
                    break;
                default:
                    break;

            }
                
        }

        private string SetTimeColor(string prevColor, string useColor)
        {
            if (prevColor.Contains(Color.White.ToString()))
            {
                return useColor;
            }
            else
            {
                return Color.Blue.ToString();
            }
        }

        private void SetOne(string useColor = "White")
        {
            if (this.ticks % 2 == 0)
            {
                OneO = SetTimeColor(OneO, useColor);
            }
            else
            {
                OneU = SetTimeColor(OneU, useColor);
            }
        }

        private void SetTwo( string useColor = "White")
        {
            if (this.ticks % 2 == 0)
            {
                Two = SetTimeColor(Two, useColor);
            }
            else
            {
                OneO = SetTimeColor(OneO, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }

        private void SetThree( string useColor = "White")
        {
            if (this.ticks % 3 == 0)
            {
                Three = SetTimeColor(Three, useColor);
            }
            else if(ticks % 3 == 1)
            {
                Two = SetTimeColor(Two, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else 
            {
                Two = SetTimeColor(Two, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }

        private void SetFour(string useColor = "White")
        {
            if (this.ticks % 3 == 0)
            {
                Three = SetTimeColor(Three, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else if (this.ticks % 3 == 1)
            {
                Three = SetTimeColor(Three, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else
            {
                Two = SetTimeColor(Two, useColor);
                OneU = SetTimeColor(OneU, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
        }

        private void SetFive(string useColor = "White")
        {
            if (this.ticks % 3 == 0)
            {
                Five = SetTimeColor(Five, useColor);
            }
            else if (this.ticks % 3 == 1)
            {
                Three = SetTimeColor(Three, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else
            {
                Two = SetTimeColor(Two, useColor);
                OneU = SetTimeColor(OneU, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
        }

        private void SetSix(string useColor = "White")
        {
            if (this.ticks % 4 == 0)
            {
                Five = SetTimeColor(Five, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else if (this.ticks % 4 == 1)
            {
                Five = SetTimeColor(Five, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
            else if (this.ticks % 4 == 2)
            {
                Three = SetTimeColor(Three, useColor);
                Two = SetTimeColor(Two, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else
            {
                Three = SetTimeColor(Three, useColor);
                Two = SetTimeColor(Two, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }

        private void SetSeven(string useColor = "White")
        {
            if (this.ticks % 3 == 0)
            {
                Five = SetTimeColor(Five, useColor);
                Two = SetTimeColor(Two, useColor);
            }
            else if (this.ticks % 3 == 1)
            {
                Five = SetTimeColor(Five, useColor);
                OneU = SetTimeColor(OneU, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else 
            {
                Three = SetTimeColor(Three, useColor);
                Two = SetTimeColor(Two, useColor);
                OneO = SetTimeColor(OneO, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }

        private void SetEight(string useColor = "White")
        {
            if (this.ticks % 3 == 0)
            {
                Five = SetTimeColor(Five, useColor);
                Three = SetTimeColor(Three, useColor);
            }
            else if (this.ticks % 3 == 1)
            {
                Five = SetTimeColor(Five, useColor);
                Two = SetTimeColor(Two, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else
            {
                Five = SetTimeColor(Five, useColor);
                Two = SetTimeColor(Two, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }

        private void SetNine(string useColor = "White")
        {
            if (this.ticks % 3 == 0)
            {
                Five = SetTimeColor(Five, useColor);
                Three = SetTimeColor(Three, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else if (this.ticks % 3 == 1)
            {
                Five = SetTimeColor(Five, useColor);
                Three = SetTimeColor(Three, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
            else
            {
                Five = SetTimeColor(Five, useColor);
                Two = SetTimeColor(Two, useColor);
                OneO = SetTimeColor(OneO, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }

        private void SetTen(string useColor = "White")
        {
            if (this.ticks % 2 == 0)
            {
                Five = SetTimeColor(Five, useColor);
                Three = SetTimeColor(Three, useColor);
                Two = SetTimeColor(Two, useColor);
            }
            else
            {
                Five = SetTimeColor(Five, useColor);
                Three = SetTimeColor(Three, useColor);
                OneO = SetTimeColor(OneO, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }
        private void SetEleven(string useColor = "White")
        {
            if (this.ticks % 2 == 0)
            {
                Five = SetTimeColor(Five, useColor);
                Three = SetTimeColor(Three, useColor);
                Two = SetTimeColor(Two, useColor);
                OneO = SetTimeColor(OneO, useColor);
            }
            else
            {
                Five = SetTimeColor(Five, useColor);
                Three = SetTimeColor(Three, useColor);
                Two = SetTimeColor(Two, useColor);
                OneU = SetTimeColor(OneU, useColor);
            }
        }
        private void OnNotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
