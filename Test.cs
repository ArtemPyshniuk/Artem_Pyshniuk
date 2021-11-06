using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Test
{ 
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestTask1GetAllIntegers()
        {
            Assert.AreEqual(Program.GetIntegersFromList(new List<object>() { 1, 2, "a", "b" }), new List<object>() { 1, 2 });
            Assert.AreEqual(Program.GetIntegersFromList(new List<object>() { 1, 2 }), new List<object>() { 1, 2 });
            Assert.AreEqual(Program.GetIntegersFromList(new List<object>() { "a", "b", 1, "1", "111" }), new List<object>() { 1 });
        }

        [Test]
        public void TestTask1GetNothing()
        {
            Assert.AreEqual(Program.GetIntegersFromList(new List<object>() { "a", "b" }), new List<object>() { });
            Assert.AreEqual(Program.GetIntegersFromList(new List<object>() { 1.2, 2.6 }), new List<object>() { });
        }

        [Test]
        public void TestTask2GetLetter()
        {
            Assert.AreEqual(Program.FirstNonRepeatingLetter("stReSS"), "t");
            Assert.AreEqual(Program.FirstNonRepeatingLetter("abDabc"), "D");
            Assert.AreEqual(Program.FirstNonRepeatingLetter("a"), "a");
        }

        [Test]
        public void TestTask2GetNothing()
        {
            Assert.AreEqual(Program.FirstNonRepeatingLetter("abccba"), "");
            Assert.AreEqual(Program.FirstNonRepeatingLetter("aa"), "");
        }

        [Test]
        public void TestTask3GetRootFromOneDigitNumber()
        {
            Assert.AreEqual(Program.DigitalRoot(6), 6);
            Assert.AreEqual(Program.DigitalRoot(1), 1);
        }

        [Test]
        public void TestTask3GetRootFromMoreThanOneDigitNumber()
        {
            Assert.AreEqual(Program.DigitalRoot(16), 7);
            Assert.AreEqual(Program.DigitalRoot(123), 6);
            Assert.AreEqual(Program.DigitalRoot(942), 6);
            Assert.AreEqual(Program.DigitalRoot(493193), 2);
        }

        [Test]
        public void TestTask4GetZeroPairs()
        {
            Assert.AreEqual(Program.PairsCount(new int[] { 1, 2, 5 }, 4), 0);
            Assert.AreEqual(Program.PairsCount(new int[] { 1 }, 1), 0);
        }

        [Test]
        public void TestTask4GetPairs()
        {
            Assert.AreEqual(Program.PairsCount(new int[] { 1, 2, 5 }, 7), 1);
            Assert.AreEqual(Program.PairsCount(new int[] { 1, 1, 3, 1 }, 4), 3);
        }

        [Test]
        public void TestTask5GetSortedNames()
        {
            Assert.AreEqual(Program.SortedSurnamesNames("Fired:Corwill;Wilfred:Corwill;Barney:TornBull;Betty:Tornbull;Bjon:Tornbull;Raphael:Corwill;Alfred:Corwill"),
                                                        "(CORWILL, ALFRED) (CORWILL, FIRED) (CORWILL, RAPHAEL) (CORWILL, WILFRED) (TORNBULL, BARNEY) (TORNBULL, BETTY) (TORNBULL, BJON)");

        }

        [Test]
        public void TestTask5GetSortedEqualNames()
        {
            Assert.AreEqual(Program.SortedSurnamesNames("Fired:Corwill;Betty:Tornbull;Bjon:Tornbull;Fired:Corwill"),
                                                        "(CORWILL, FIRED) (CORWILL, FIRED) (TORNBULL, BETTY) (TORNBULL, BJON)");

        }

        [Test]
        public void TestTask6GetNegativeResult()
        {
            Assert.AreEqual(Program.NextBiggerNumber(321), -1);
            Assert.AreEqual(Program.NextBiggerNumber(1), -1);
            Assert.AreEqual(Program.NextBiggerNumber(333), -1);
            Assert.AreEqual(Program.NextBiggerNumber(100000), -1);
        }

        [Test]
        public void TestTask6GetResult()
        {
            Assert.AreEqual(Program.NextBiggerNumber(12), 21);
            Assert.AreEqual(Program.NextBiggerNumber(135), 153);
            Assert.AreEqual(Program.NextBiggerNumber(2017), 2071);
            Assert.AreEqual(Program.NextBiggerNumber(270821), 278012);
        }

        [Test]
        public void TestTask7GetZeroIP()
        {
            Assert.AreEqual(Program.UInt32ToIP(0), "0.0.0.0");

        }

        [Test]
        public void TestTask7GetIP()
        {
            Assert.AreEqual(Program.UInt32ToIP(32), "0.0.0.32");
            Assert.AreEqual(Program.UInt32ToIP(2149583361), "128.32.10.1");

        }

    }

    class Program
    {
        public static IEnumerable<int> GetIntegersFromList(List<object> listOfItems)
        {
            List<int> resultList = new List<int>();
            for (int i = 0; i < listOfItems.Count; i++)
            {
                if (listOfItems[i] is int)
                {
                    resultList.Add(Convert.ToInt32(listOfItems[i]));
                }
            }

            return resultList;
        }

        public static string FirstNonRepeatingLetter(string s)
        {
            char[] letters = s.ToCharArray();
            foreach (char letter in letters)
            {
                int i = 0;
                foreach (char l in s)
                    if (Char.ToLower(letter) == Char.ToLower(l))
                        i++;

                if (i == 1) return Char.ToString(letter);
            }
            return "";
        }

        public static long DigitalRoot(long n)
        {
            while (n / 10 != 0)
            {
                string str = n.ToString();

                int[] arrayOfDigits = str.Select(x => (int)char.GetNumericValue(x)).ToArray();

                n = arrayOfDigits.Sum();

            }

            return n;
        }

        public static int PairsCount(int[] arr, int target)
        {
            int amount = 0;

            for (int i = 0; i < arr.Length; i++)
                for (int j = i + 1; j < arr.Length; j++)
                    if (arr[i] + arr[j] == target)
                        amount++;

            return amount;
        }

        public static string SortedSurnamesNames(string str)
        {
            var namesSurnamesArr = str.ToUpper().Split(';');

            List<string> surnamesNamesList = new List<string>();

            foreach (string nameSurname in namesSurnamesArr)
            {
                string[] nameSurnameArr = nameSurname.Split(':');
                string surnameName = "(" + nameSurnameArr[1] + ", " + nameSurnameArr[0] + ")";
                surnamesNamesList.Add(surnameName);
            }

            surnamesNamesList = surnamesNamesList.OrderBy(q => q).ToList();

            return string.Join(" ", surnamesNamesList);
        }

        public static long NextBiggerNumber(long n)
        {
            string str = n.ToString();
            int[] arrayOfDigits = str.Select(x => (int)char.GetNumericValue(x)).ToArray();

            int dtc1 = 0;
            int dtc2 = 0;

            for (int i = 0; i < arrayOfDigits.Length - 1; i++)
            {
                if (arrayOfDigits[i + 1] > arrayOfDigits[i])
                {
                    dtc1 = i;
                    dtc2 = i + 1;
                }
            }

            if (dtc1 == dtc2) return -1;

            int temp = arrayOfDigits[dtc1];
            arrayOfDigits[dtc1] = arrayOfDigits[dtc2];
            arrayOfDigits[dtc2] = temp;

            Array.Sort(arrayOfDigits, dtc2, arrayOfDigits.Length - dtc2);

            string s = "";
            foreach (int digit in arrayOfDigits)
                s = s + digit.ToString();

            return Convert.ToInt64(s);
        }

        public static string UInt32ToIP(uint ip)
        {
            return (ip >> 24).ToString() + "." +
             (ip << 8 >> 24).ToString() + "." +
             (ip << 16 >> 24).ToString() + "." +
             (ip << 24 >> 24).ToString();
        }
    }

}
