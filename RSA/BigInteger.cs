using System;

namespace RSA
{
    class BigInteger:IComparable<BigInteger>,IEquatable<BigInteger>
    {

        
        //====================
        //Your Code is Here:
        //===================
        /// <summary>
        /// TODO: Write an efficient algorithm that multiplies 2 BigIntegers (A and B)
        /// </summary>
        /// <param name="X">first Operand, Non-Negative BigInteger</param>
        /// <param name="Y">second Operand, Non-Negative BigInteger</param>
        /// <returns>BigInteger, The result of Multiplying A by B</returns>
        public static BigInteger Multiply(BigInteger X, BigInteger Y)
        {
            BigInteger total = new BigInteger("0");
            BigInteger Ten = new BigInteger("10");
            //BigInteger One = new BigInteger("1");
            int intval;
            if (X.CompareTo(Ten) == -1 || Y.CompareTo(Ten) == -1)
            {
                if (X.CompareTo(Ten) == -1)
                {

                    intval = Convert.ToInt32(X.Number_getter());
                    for (int i = 0; i < intval; i++)
                    {
                        total = Add(total, Y);
                    }
                    return total;
                }
                else
                {
                    intval = Convert.ToInt32(Y.Number_getter());
                    for (int i = 0; i < intval; i++)
                    {
                        total = Add(total, X);
                    }
                    return total;
                }


            }
            int num1 = X.Number.Length;
            int num2 = Y.Number.Length;
            int max2 = 0;
            int n = Math.Max(num1, num2);

            if (n % 2 == 1)
            {
                if (n == num1)
                {
                    X = AddLeadingZeros(X, 1);


                }
                else if (n == num2)
                {
                    Y = AddLeadingZeros(Y, 1);

                }
                n += 1;
            }
            if (X.Number.Length != Y.Number.Length)
            {
                if (X.Number.Length < Y.Number.Length)
                {
                    X = AddLeadingZeros(X, (Y.Number.Length - X.Number.Length));
                }
                else
                {
                    Y = AddLeadingZeros(Y, (X.Number.Length - Y.Number.Length));
                }
            }
            //n = n / 2 + n % 2;
            if (X.Number.Length == Y.Number.Length)
            {
                BigInteger a = First_Half(X, n);
                BigInteger b = Second_Half(X, n);
                BigInteger c = First_Half(Y, n);
                BigInteger d = Second_Half(Y, n);

                BigInteger first = Multiply(a, c);
                BigInteger second = Multiply(b, d);
                BigInteger third = Multiply(Add(a, b), Add(c, d));
                BigInteger fourth = Subtract(Subtract(third, first), second);

                BigInteger result = PadWithZeros(first, n) + second + PadWithZeros(fourth, n / 2);
                return result;
            }
            throw new NotImplementedException();
        }
        public static BigInteger First_Half(BigInteger A, int a)
        {
            int n = A.GetDigitsCount();
            String first_half = "";
            String s = A.Number_getter();

            for (int i = 0; i < n / 2; i++)
            {
                first_half += s[i];

            }

            BigInteger result = new BigInteger(first_half);
            // PadWithZeros(result, a);

            return result;

        }
        public static BigInteger Second_Half(BigInteger B, int a)
        {
            int n = B.GetDigitsCount();
            String second_half = "";
            String s = B.Number_getter();


            for (int i = n / 2; i < n; i++)
            {
                second_half += s[i];

            }
            BigInteger result = new BigInteger(second_half);
            //AddLeadingZeros(result, a);
            return result;

        }

        //====================
        //Your Code is Here:
        //===================
        /// <summary>
        /// TODO: Write an efficient algorithm that calculates div-mod
        /// The function should calculates the quotient (A / B) and the remainder (A mod B)
        /// </summary>
        /// <param name="A">the dividend, non-negative BigInteger</param>
        /// <param name="B">the divisor, Positive BigInteger</param>
        /// <returns> 
        /// A Tuple (pair of BigIntegers)
        /// The first Item is the quotient (A / B)
        /// The second Item is the remainder (A mod B)
        /// </returns>
        public static Tuple<BigInteger, BigInteger> DivMod(BigInteger A, BigInteger B)
        {
            BigInteger quotient = new BigInteger("0");
            BigInteger one = new BigInteger("1");
            BigInteger two = new BigInteger("2");

            if (A.CompareTo(B) == -1)
            {
                return new Tuple<BigInteger, BigInteger>(quotient, A);
            }
            Tuple<BigInteger, BigInteger> answer = DivMod(A, Add(B, B));// Add(B, B)
            BigInteger q = (Add(answer.Item1, answer.Item1)); //
                                                              //answer.Item1.Number = q.Number;
           
            if (answer.Item2.CompareTo(B) == -1)
            {
                return new Tuple<BigInteger, BigInteger>(q, answer.Item2); //answer.Item1 answer.Item1
            }
            else
            {
                return new Tuple<BigInteger, BigInteger>(Add(q, one), Subtract(answer.Item2, B)); //answer.Item1 + one answer.Item2-B
            }
            throw new NotImplementedException();
        }

        //====================
        //Your Code is Here:
        //===================
        /// <summary>
        /// TODO: Write an efficient algorithm that calculates Mod of Power
        /// The function should calculates the result of the equation (B ^ P) mod M
        /// </summary>
        /// <param name="B">the base, non-negative BigInteger</param>
        /// <param name="P">the exponent, non-negative BigInteger</param>
        /// <param name="M">the modulus value, positive BigInteger</param>
        /// <returns>BigInteger, The result of (B ^ P) mod M</returns>
        public static BigInteger ModOfPower(BigInteger B, BigInteger P, BigInteger M)
        {
            BigInteger zero = new BigInteger("0");
            BigInteger two = new BigInteger("2");
            
            if (P.Equals(zero))
            {
                return one;
            }
            
            
                Tuple<BigInteger, BigInteger> ans = DivMod(P, two);
                BigInteger S = ans.Item1;
                BigInteger result = ModOfPower(B, S, M);
                Tuple<BigInteger, BigInteger> result2 = DivMod(result, M);
                BigInteger result3 = result2.Item2;
                BigInteger result4 = (Multiply(result3, result3));
                Tuple<BigInteger, BigInteger> result5 = DivMod(result4, M);
                BigInteger result6 = result5.Item2;

                if (Is_Even(P) == false)
                {
                    result6 = result6 * B;
                }
                Tuple<BigInteger, BigInteger> result7 = DivMod(result6, M);
                return result7.Item2;

            //WORKING TILL CASE 5
                /*else
                 {
                     Tuple<BigInteger, BigInteger> ans = DivMod(B,M);
                     BigInteger sol = Multiply(ans.Item2,P);
                     Tuple<BigInteger, BigInteger> sol2 = DivMod(sol, M);
                     BigInteger A = sol2.Item2;
                     if (Is_Even(P) == false)
                     {
                         A = Multiply(A, B);
                     }
                     Tuple<BigInteger, BigInteger> C = DivMod(A, M);
                     return C.Item2;
                 }*/


            throw new NotImplementedException();
        }

        #region "Constructors"
        /// <summary>
        /// Default Constructor
        /// Creates A BigInteger that has the value of zero
        /// </summary>
        public BigInteger()
        {
            Sign = false;
            Number = new byte[1] { 0 };
        }
        /// <summary>
        /// Creates a BigInteger from a numeric string
        /// </summary>
        /// <param name="number">Numeric string</param>
        public BigInteger(string number)
        {
            while (number[0] == '0' && number.Length != 1)
                number = number.Remove(0, 1);

            if (char.IsDigit(number[0]))
            {
                Number_setter(number);
                Sign = false;
            }
            else
            {
                if (number[0] == '-')
                {
                    Sign = true;
                    Number_setter(number.Substring(1));
                }
                else if (number[0] == '+')
                {
                    Sign = false;
                    Number_setter(number.Substring(1));
                }
                else
                {
                    Sign = false;
                    Number_setter(number);
                }
            }
        }
        /// <summary>
        /// Creates a BigInteger from a numeric String and a Sign
        /// </summary>
        /// <param name="number">Numeric string</param>
        /// <param name="sign">Boolean variable for the sign
        /// (sign = true for negatives, sign = false for positives)</param>
        public BigInteger(string number, bool sign)
        {
            while (number[0] == '0' && number.Length != 1)
                number = number.Remove(0, 1);
            Number_setter(number);
            Sign_setter(sign);
        }
        /// <summary>
        /// Creates a BigInteger form a byte array
        /// Each element in the array represents a digit in the number
        /// </summary>
        /// <param name="arr">byte array. Each digit represents a digit of the big number</param>
        public BigInteger(byte[] arr)
        {
            this.Number = arr;
        }
        /// <summary>
        /// Creates a BigInteger from a byte array and a sign
        /// Each element in the array represents a digit in the number
        /// </summary>
        /// <param name="arr">byte array. Each digit represents a digit of the big number</param>
        /// <param name="sign">Boolean variable for the sign
        /// (sign = true for negatives, sign = false for positives)</param>
        public BigInteger(byte[] arr, bool sign)
        {
            this.Number = arr;
            this.Sign = sign;
        }
        /// <summary>
        /// Creates a BigInteger from a variable with long data type
        /// </summary>
        /// <param name="number">the number whose value will be in the big number</param>
        public BigInteger(long number)
        {
            if (number < 0)
                Sign = true;
            else
                Sign = false;

            String s;
            s = number.ToString();

            Number_setter(s);
        }
        #endregion

        public void Number_setter(string number)
        {
            if (number[0] == '-')
            {
                Sign_setter(true);
                Number = System.Text.Encoding.UTF8.GetBytes(number.Substring(1));
            }
            else
                Number = System.Text.Encoding.UTF8.GetBytes(number);
            for(int i = 0; i < Number.Length; i++)
            {
                Number[i] = (byte)(Number[i] - '0');
            }
        }
        public void Sign_setter(bool sign)
        {
            Sign = sign;
        }
        public string Number_getter()
        {
            string ret = "";
            for(int i = 0; i < Number.Length; i++)
            {
                ret = ret + ((char)(Number[i] + '0'));
            }
            return ret;
        }
        public bool Sign_getter()
        {
            return Sign;
        }
        public int GetDigitsCount()
        {
            return Number.Length;
        }
        

        #region "Privates"

        private bool Sign;

        public byte[] Number { get; set; }
        #endregion

        /// <summary>
        /// Add two positive BigIntegers and returns the result
        /// </summary>
        /// <param name="A">Big Integer (should be positive)</param>
        /// <param name="B">Big Integer (should be positive)</param>
        /// <returns>Big Integer (the result of adding A and B)</returns>
        public static BigInteger Add(BigInteger A, BigInteger B)
        {
            A = RemoveLeadingZeros(A);
            B = RemoveLeadingZeros(B);
            if (B.Number.Length > A.Number.Length)
            {
                byte[] tmp = A.Number;
                A.Number = B.Number;
                B.Number = tmp;
            }

            B = BigInteger.AddLeadingZeros(B, A.GetDigitsCount() - B.GetDigitsCount());
            byte[] sum = new byte[A.Number.Length];

            byte carry = 0;


            int cntr = A.Number.Length - 1;
            for (int i = B.Number.Length - 1; i >= 0; i--)
            {
                byte tmp = (byte)(A.Number[cntr] + B.Number[i] + carry);
                if (tmp > 9)
                {
                    tmp -= 10;
                    carry = 1;
                }
                else
                    carry = 0;

                sum[cntr] = tmp;
                cntr--;
            }

            while (carry == 1)
            {
                if (cntr == -1)
                {
                    byte[] extendedSum = new byte[sum.Length + 1];
                    extendedSum[0] = 1;
                    for (int i = 1; i <= sum.Length; i++)
                    {
                        extendedSum[i] = sum[i - 1];
                    }
                    return new BigInteger(extendedSum);
                }


                byte tmp = (byte)(sum[cntr] + 1);
                if (tmp > 9)
                {
                    tmp -= 10;
                    carry = 1;
                }
                else
                    carry = 0;

                sum[cntr] = tmp;
                cntr--;
            }


            return new BigInteger(sum);

        }

        /// <summary>
        /// Subtracts B from A, A and B are positive BigIntegers
        /// </summary>
        /// <param name="A">First Operand</param>
        /// <param name="B">Second Operand</param>
        /// <returns>Subtraction Result as a BigInteger (It may be negative)</returns>
        public static BigInteger Subtract(BigInteger A, BigInteger B)
        {

            bool swap = false;
            A = RemoveLeadingZeros(A);
            B = RemoveLeadingZeros(B);
            if (A.CompareTo(B) < 0)
            {
                BigInteger tmp = A;
                A = B;
                B = tmp;
                swap = true;
            }
            else if (A.Equals(B) == true)
            {
                return new BigInteger();
            }

            if (A.Number.Length > B.Number.Length)
            {
                B = AddLeadingZeros(B, A.Number.Length - B.Number.Length);
            }
            else
            {
                A = AddLeadingZeros(A, B.Number.Length - A.Number.Length);
            }

            BigInteger res = new BigInteger(A.Number);


            for (int i = A.Number.Length - 1; i >= 0; i--)
            {
                if (A.Number[i] < B.Number[i])
                {
                    int j = i - 1;
                    for(; j >= 0; j--)
                    {
                        if(A.Number[j] > 0)
                        {
                            A.Number[j]--;
                            j++;
                            break;
                        }
                    }
                    while(j < i)
                    {
                        A.Number[j] = 9;
                        j++;
                    }
                    A.Number[j] += 10;
                    
                }
                
                 res.Number[i] = (byte)(A.Number[i] - B.Number[i]);
                
            }

            res = RemoveLeadingZeros(res);

            if (swap)
                res.Sign = true;


            return res;
        }

        #region "Operators"
        public static BigInteger operator -(BigInteger A, BigInteger B)
        {
            BigInteger result = Subtract(A, B);
            return result;
        }
        public static BigInteger operator +(BigInteger A, BigInteger B)
        {
            return Add(A, B);
        }
        public static BigInteger operator *(BigInteger A, BigInteger B)
        {
            return Multiply(A, B);
        }
        #endregion

        #region Static Data Members
        /// <summary>
        /// a big integer with value equal to zero
        /// to be used as a helper object during the implementation (e.g. to compare bigintegers with it)
        /// </summary>
        public static BigInteger zero = new BigInteger();
        /// <summary>
        /// a big integer with value equal to one
        /// to be used as a helper object during the implementation (e.g. to add it to another BigInteger)
        /// </summary>
        public static BigInteger one = new BigInteger(new byte[] { 1 });
        /// <summary>
        /// a big integer with value equal to two
        /// to be used as a helper object during the implementation (e.g. to multiply it with another bigInteger)
        /// </summary>
        public static BigInteger two = new BigInteger(new byte[] { 2 });
        #endregion

        /// <summary>
        /// Calculates the parity of the big integer (is it odd or even?)
        /// </summary>
        /// <param name="bigInteger">the big integer</param>
        /// <returns>
        /// true if the integer is even.
        /// false if the integer is odd.
        /// </returns>
        public static bool Is_Even(BigInteger bigInteger)
        {
            return bigInteger.Number[bigInteger.Number.Length - 1] % 2 == 0;
        }

        /// <summary>
        /// Check if the big integer equals zero or not
        /// </summary>
        /// <param name="bigInteger">a big integer</param>
        /// <returns>
        /// true if the big integer is zero
        /// false otherwise
        /// </returns>
        public static bool Is_Zero(BigInteger bigInteger)
        {
            for(int i = 0; i < bigInteger.Number.Length; i++)
            {
                if(bigInteger.Number[i] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void Display_Biginteger()
        {
            if (Sign_getter())
                Console.Write('-');
            Console.WriteLine(Number_getter());
        }

        /// <summary>
        /// pad the big integer with zeros (to the right)
        /// for example: if A = 29398, numberOfZeros = 5
        /// the funciton should return : 2939800000
        /// </summary>
        /// <param name="A">big integer</param>
        /// <param name="numberOfZeros">the number of zeros to pad</param>
        /// <returns>the big integer after padding it with zeros</returns>
        public static BigInteger PadWithZeros(BigInteger A, int numberOfZeros)
        {
            byte[] arr = new byte[A.Number.Length + numberOfZeros];
            for (int i = 0; i < arr.Length; i++)
            {
                if(i < A.Number.Length)
                {
                    arr[i] = A.Number[i];
                }
                else
                {
                    arr[i] = 0;
                }
            }

            return new BigInteger(arr);
        }

        /// <summary>
        /// add trailing zeros to the big integer (to the left)
        /// for example: if A = 29398, numberOfZeros = 5
        /// the funciton should return a big integer with the value: 0000029398
        /// </summary>
        /// <param name="A">big integer</param>
        /// <param name="numberOfZeros">the number of zeros to add</param>
        /// <returns>the big integer after adding trailing zeros</returns>
        public static BigInteger AddLeadingZeros(BigInteger A, int numberOfZeros)
        {
            byte[] arr = new byte[A.Number.Length + numberOfZeros];
            for (int i = 0; i < arr.Length; i++)
            {
                if (i < numberOfZeros)
                {
                    arr[i] = 0;
                }
                else
                {
                    arr[i] = A.Number[i - numberOfZeros];
                }
            }

            return new BigInteger(arr);
        }

        /// <summary>
        /// remove trailing zeros from the big integer
        /// for example: if A = 0000293
        /// the funciton should return 293
        /// </summary>
        /// <param name="A">a big integer, to remove the trailing zeros from</param>
        /// <returns>the number after removing trailing zeros</returns>
        public static BigInteger RemoveLeadingZeros(BigInteger A)
        {
            BigInteger res;
            int firstNonZeroIndex = -1;
            for (int i = 0; i < A.Number.Length; i++)
            {
                if (A.Number[i] != 0)
                {
                    firstNonZeroIndex = i;
                    break;
                }
            }

            if (firstNonZeroIndex == -1) // the number is zero
            {
                res = new BigInteger();
            }
            else
            {
                byte[] arr = new byte[A.Number.Length - firstNonZeroIndex];
                for (int i = firstNonZeroIndex; i < A.Number.Length; i++)
                {
                    arr[i - firstNonZeroIndex] = A.Number[i];
                }
                res = new BigInteger(arr);
            }

            return res;
        }

        /// <summary>
        /// Compare the instance of BigInteger with other Biginteger
        /// </summary>
        /// <param name="other">another BigInteger, to compare the current BigInteger with</param>
        /// <returns>
        /// -1 if this instance is less than other
        /// 0 if this instance is equal to other
        /// 1 if this instance is greater than other
        /// </returns>
        public int CompareTo(BigInteger other)
        {
            BigInteger A = RemoveLeadingZeros(this);
            this.Number = A.Number;
            other = RemoveLeadingZeros(other);
            if (other.Sign == true && this.Sign == true)
            {
                if (this.Number.Length < other.Number.Length)
                {
                    return 1;
                }
                else if (this.Number.Length > other.Number.Length)
                {
                    return -1;
                }
                else
                {
                    for (int i = 0; i < this.Number.Length; i++)
                    {
                        if (this.Number[i] < other.Number[i])
                        {
                            return 1;
                        }
                        else if (this.Number[i] > other.Number[i])
                        {
                            return -1;
                        }
                    }

                    return 0;
                }
            }
            else if (other.Sign == true && this.Sign == false)
            {
                return 1;
            }
            else if (other.Sign == false && this.Sign == true)
            {
                return -1;
            }
            else // both are false (positive)
            {
                if(this.Number.Length < other.Number.Length)
                {
                    return -1;
                }
                else if(this.Number.Length > other.Number.Length)
                {
                    return 1;
                }
                else
                {
                    for(int i = 0; i < this.Number.Length; i++)
                    {
                        if(this.Number[i] < other.Number[i])
                        {
                            return -1;
                        }
                        else if (this.Number[i] > other.Number[i])
                        {
                            return 1;
                        }
                    }

                    return 0;
                }
            }
        }

        /// <summary>
        /// Check if the instance of BigInteger and other BigInteger are equal or not
        /// </summary>
        /// <param name="other">another BigInteger, to check the equality of it with current BigInteger</param>
        /// <returns>
        /// true if this instanc is equal to other
        /// false if they are not equal
        /// </returns>
        public bool Equals(BigInteger other)
        {
            Number = RemoveLeadingZeros(new BigInteger(Number)).Number;
            other = RemoveLeadingZeros(other);
            if(Sign != other.Sign || other.Number.Length != Number.Length)
            {
                return false;
            }
            else
            {
                for(int i = 0; i < Number.Length; i++)
                {
                    if(Number[i] != other.Number[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
