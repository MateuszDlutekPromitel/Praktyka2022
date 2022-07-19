namespace promitel1.common
{
    public static class TestClass
    {
        /// <summary>
        /// multiplays minus values by <c>-1</c>
        /// </summary>
        /// <param name="number">the number that we will get the absolute value of</param>
        /// <returns>returns absolute value of the number</returns>
        public static int Test1(int number)
        {
            if (number <= 0) number *= -1;
            return number;
        }




    }
}
