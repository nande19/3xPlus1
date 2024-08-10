using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3xPlus1
{
    public class T3XPlus1cs
    {
        /// <summary>
        /// List Holds the Data Point Generated
        /// </summary>
        /// 

        private readonly List<long> DataList;

        /// <summary>
        /// Give access to the local data structure 
        /// 

        public List<long> DataListPublic => this.DataList;
        //--------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Default Constructor 
        /// </summary>

        public T3XPlus1cs()
        {
            this.DataList = new List<long>();
        }
        //--------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method returns the DataList
        /// </summary>
        public bool ProcessAlgorithm(long seedValue)
        {
            this.DataList.Clear();
            this.DataList.Add(seedValue);

            try
            {
                while (seedValue != 1)
                {
                    if ((seedValue % 2) == 0)
                    {
                        seedValue /= 2;
                    }
                    else
                    {
                        seedValue = (3 * seedValue) + 1;
                    }
                    this.DataList.Add(seedValue);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }
    }
}