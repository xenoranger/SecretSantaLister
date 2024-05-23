using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using SecretSantaCore.Models;

namespace SecretSantaCore
{
    public class SantaLister
    {
        List<Recipient> recipients = new List<Recipient>();
        List<Recipient> givers = new List<Recipient>();
        List<Recipient> elligible = new List<Recipient>();
        List<FamilyGroup> familyGroups = new List<FamilyGroup>();
        List<Recipient2Giver> matchedPairs = new List<Recipient2Giver>();
        Random rand = new Random();


        public SantaLister()
        {

            recipients.Add(new Recipient { Name = "Nate", FamilyNumb = 7,randNum = rand.Next() });

            //            recipients.Add(new Recipient { Name = "Meg", FamilyNumb = 9,randNum = rand.Next()  });

            recipients.Add(new Recipient { Name = "Carole" , FamilyNumb = 1, randNum = rand.Next() });

            recipients.Add(new Recipient { Name = "Dave", FamilyNumb = 2, randNum = rand.Next() });

            recipients.Add(new Recipient { Name = "Zach", FamilyNumb = 3, randNum = rand.Next() });

            recipients.Add(new Recipient { Name = "Bethany", FamilyNumb = 4, randNum = rand.Next() });

            recipients.Add(new Recipient { Name = "Steve", FamilyNumb = 5, randNum = rand.Next() });

            recipients.Add(new Recipient { Name = "Debbie", FamilyNumb = 1, randNum = rand.Next() });
            
            recipients.Add(new Recipient { Name = "Donna", FamilyNumb = 2, randNum = rand.Next() });
            
            
            recipients.Add(new Recipient { Name = "Sarah", FamilyNumb = 3, randNum = rand.Next() });
            
            
            recipients.Add(new Recipient { Name = "Doreen", FamilyNumb = 5, randNum = rand.Next() });

            
            recipients.Add(new Recipient { Name = "Carlos", FamilyNumb = 4, randNum = rand.Next() });

            recipients.Add(new Recipient { Name = "Gabe", FamilyNumb = 6, randNum = rand.Next() });
            
            //            recipients.Add(new Recipient { Name = "Amanda", FamilyNumb = 8 });





        }



        public bool Run()
        {
            int maxtries = 20;
            bool matchedAll = false;

            List<Recipient> recipientsMix1 = new List<Recipient>();
            List<Recipient> recipientsMix2 = new List<Recipient>();

            foreach (var recipient in recipients)
            {
                recipientsMix2.Add(recipient);
                recipientsMix1.Add(recipient);

            }

            matchedPairs.Clear();



            for (int i = 0; i < maxtries; i++)
            {

                var availableRecipients = new List<Recipient>();
                var availableGivers = new List<Recipient>();

                foreach (var recipient in recipients)
                {
                    availableGivers.Add(recipient);
                    availableRecipients.Add(recipient);
                }


                foreach (var currentGiver in availableGivers)
                {
                    var currentRecipient = availableRecipients.Where(p => p.FamilyNumb != currentGiver.FamilyNumb).Where(n => n.Name != currentGiver.Name).OrderBy(o => o.randNum).LastOrDefault();
                    matchedPairs.Add(new Recipient2Giver(recipient: currentRecipient, giver: currentGiver));
                    availableRecipients.Remove(availableRecipients.Where(r => r.Name == currentRecipient.Name).Where(p => p.randNum == currentRecipient.randNum).FirstOrDefault());


                }



                if (matchedPairs.Count == recipients.Count)
                {
                    foreach (var matchResult in matchedPairs)
                    {
                        Console.WriteLine($"Giver {matchResult.Giver.Name} -> Recipient: {matchResult.Receiver.Name}");
                    }

                    matchedAll = true;
                    break;
                }
            }





            return matchedAll;



        }

        public bool RunOld()
        {

            int rndNumb;

            List<Recipient> recipientsMix1 = new List<Recipient>();
            List<Recipient> recipientsMix2 = new List<Recipient>();

            foreach(var recipient in recipients)
            {
                recipientsMix2.Add(recipient);
                recipientsMix1.Add(recipient);

            }

            Console.WriteLine($"Recipients1\t{recipientsMix1.Count.ToString()}");
            Console.WriteLine($"Recipients2\t{recipientsMix2.Count.ToString()}");


            // Needs an Odd Number
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine("Running Even");

                    recipientsMix1.Clear();

                    foreach (var recipient in recipientsMix2)
                    {
                        recipientsMix1.Add(recipient);

                    }

                    recipientsMix2.Clear();
                    
                    while (recipientsMix2.Count <= recipients.Count)
                    {
                        if (recipientsMix1.Count == 0) break;
                        rndNumb = rand.Next(0, (recipientsMix1.Count - 1 < 1)? 0: recipientsMix1.Count - 1);
                        Console.WriteLine($"\tRecipient1 Count\t{recipientsMix1.Count.ToString()}\n\tRecipient2 Count\t{recipientsMix2.Count.ToString()}");
                        Console.WriteLine($"\tCurrent Rnd:\t\t{rndNumb}");


                        recipientsMix2.Add(recipientsMix1[rndNumb]);
                        recipientsMix1.Remove(recipientsMix2.Last<Recipient>());
//                        recipientsMix1.Remove(recipientsMix2[recipientsMix2.Count -1 ]);

                    }

                }
                else
                {
                    Console.WriteLine("Running Odd");

                    recipientsMix2.Clear();

                    foreach (var recipient in recipientsMix1)
                    {
                        recipientsMix2.Add(recipient);

                    }

                    recipientsMix1.Clear();

                    while (recipientsMix1.Count <= recipients.Count)
                    {
                        if (recipientsMix2.Count == 0) break;

                        rndNumb = rand.Next(0, (recipientsMix2.Count - 1 < 1) ? 0 : recipientsMix2.Count - 1);
                        Console.WriteLine($"Current Rnd:\t{rndNumb}");

                        recipientsMix1.Add(recipientsMix2[rndNumb]);
                        recipientsMix2.Remove(recipientsMix1.Last<Recipient>());

                    }

                }
                       



            }


            recipients.Clear();
            foreach (var recipient in recipientsMix2)
            {
                recipients.Add(recipient);
            }




            while (matchedPairs.Count != recipients.Count)
            {
                OneMoreTime:
                givers.Clear();
                matchedPairs.Clear();
                foreach (var r in recipients)
                {
                    givers.Add(r);
                }
                foreach (var recipient in recipients)
                {
                    Console.WriteLine($"Recipient:{recipient.Name}");
                    elligible.Clear();
                    elligible = givers.Where(r => r.FamilyNumb != recipient.FamilyNumb).ToList();
                    if (elligible.Count > 0)
                    {
                        rndNumb = rand.Next(0, elligible.Count - 1);


                        if (elligible[rndNumb] != null && elligible.Count > 0)
                        {
//                            matchedPairs.Add(new Recipient2Giver { Giver = elligible[rndNumb], Receiver = recipient });
                            givers.Remove(elligible[rndNumb]);
                        }
                        else if (elligible.Count > 0)
                        {
//                            matchedPairs.Add(new Recipient2Giver { Giver = elligible.FirstOrDefault(), Receiver = recipient });

                        }
                        else
                        {
//                            matchedPairs.Add(new Recipient2Giver { Giver = new Recipient(), Receiver = recipient });

                        }

                    }

                }


            }

            foreach (var matchP in matchedPairs)
            {
                Console.WriteLine($"Giver:\t{matchP.Giver.Name}\tRecipient:\t{matchP.Receiver.Name}");
            }
            Console.WriteLine($"Total Matches:\t{matchedPairs.Count}");
            return true;
        }



    }
}
