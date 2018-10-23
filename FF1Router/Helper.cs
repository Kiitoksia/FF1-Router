using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FF1Router.Models;

namespace FF1Router
{
    public static class Helper
    {
        

        /// <summary>
        /// Given a direction, will produce the next index from the specified one
        /// </summary>
        public static int GetNextIndex(this StepCountPointerDirection direction, int currentIndex)
        {
            int newIndex = currentIndex;
            switch (direction)
            {
                case StepCountPointerDirection.Down:
                    newIndex--;
                    if (newIndex == -1) newIndex = 255; // Wrap integer to within 0-255
                    break;
                case StepCountPointerDirection.Up:
                    newIndex++;
                    if (newIndex > 255) newIndex = 0; // Wrap integer to within 0-255
                    break;
            }

            return newIndex;
        }
    }
}
