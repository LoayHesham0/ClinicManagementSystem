using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace WindowsFormsApp1
{
   public  class utils
    {
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encrypted_bytes);
        }
        public static Dictionary<int , string > getSlots()
        {
            Dictionary<int, string> slots = new Dictionary<int, string>();
            slots.Add(1, "Slot 1 : From 12:00 pm to 12:30 pm");
            slots.Add(2, "Slot 2 : From 12:30 pm to 1:00 pm");
            slots.Add(3, "Slot 3 : From 1:00 pm to 1:30 pm");
            slots.Add(4, "Slot 4 : From 1:30 pm to 2:00 pm");
            slots.Add(5, "Slot 5 : From 2:00 pm to 2:30 pm");
            slots.Add(6, "Slot 6 : From 2:30 pm to 3:00 pm");
            slots.Add(7, "Slot 7 : From 3:00 pm to 3:30 pm");
            slots.Add(8, "Slot 8 : From 3:30 pm to 4:00 pm");
            slots.Add(9, "Slot 9 : From 4:00 pm to 4:30 pm");
            slots.Add(10, "Slot 10 : From 4:30 pm to 5:00 pm");
            slots.Add(11, "Slot 11 : From 5:00 pm to 5:30 pm");
            slots.Add(12, "Slot 12 : From 5:30 pm to 6:00 pm");
            slots.Add(13, "Slot 13 : From 6:00 pm to 6:30 pm");
            slots.Add(14, "Slot 14 : From 6:30 pm to 7:00 pm");
            slots.Add(15, "Slot 15 : From 7:00 pm to 7:30 pm");
            slots.Add(16, "Slot 16 : From 7:30 pm to 8:00 pm");
            slots.Add(17, "Slot 17 : From 8:00 pm to 8:30 pm");
            slots.Add(18, "Slot 18 : From 8:30 pm to 9:00 pm");
            slots.Add(19, "Slot 19 : From 9:00 pm to 9:30 pm");
            slots.Add(20, "Slot 20 : From 9:30 pm to 10:00 pm");
            slots.Add(21, "Slot 21 : From 10:00 pm to 10:30 pm");
            slots.Add(22, "Slot 22 : From 10:30 pm to 11:00 pm");

            return slots;


        }
    }
}
