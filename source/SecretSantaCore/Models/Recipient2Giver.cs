using System;
using System.Collections.Generic;
using System.Text;
using SecretSantaCore.Models;

namespace SecretSantaCore.Models
{
    public class Recipient2Giver
    {
        public Recipient Receiver { get; set; }
        public Recipient Giver { get; set; }

        public Recipient2Giver(Recipient recipient,Recipient giver)
        {
            this.Receiver = recipient;
            this.Giver = giver;
        }

    }
}
