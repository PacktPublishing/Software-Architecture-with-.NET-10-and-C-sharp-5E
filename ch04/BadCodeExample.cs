using System;
using System.Collections.Generic;
using System.Linq;

namespace CopilotAgentSample
{
    // Bad practice: Poor naming, tight coupling, no error handling
    public class proc
    {
        // Bad: Magic numbers, no encapsulation
        public List<string> data = new List<string>();
        public int x = 0;

        // Bad: Non-descriptive method name, no validation
        public void do_stuff(string s)
        {
            data.Add(s);
            x++;
        }

        // Bad: Nested loops, inefficient algorithm, poor readability
        public string find(string val)
        {
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i].Contains(val))
                    {
                        return data[i];
                    }
                }
            }
            return "";
        }

        // Bad: Catching generic exception, swallowing errors
        public void calc()
        {
            try
            {
                int result = 10 / x;
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                // Bad: Empty catch block
            }
        }

        // Bad: God method doing multiple responsibilities
        public void ProcessData(List<string> items)
        {
            // Validation
            if (items == null) return;
            
            // Processing
            foreach (var item in items)
            {
                data.Add(item.ToUpper());
            }
            
            // Logging
            Console.WriteLine("Processed " + items.Count + " items");
            
            // Persistence
            System.IO.File.WriteAllLines("output.txt", data);
        }
    }
}