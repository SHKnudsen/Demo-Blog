using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBlog.Domain
{
    public record struct Cuisine(string Country, string FlagSrc);

    public static class Cuisines
    {
        public static Cuisine Danish => new(nameof(Danish), "https://upload.wikimedia.org/wikipedia/commons/9/9c/Flag_of_Denmark.svg");
        public static Cuisine Italian => new(nameof(Italian), "https://upload.wikimedia.org/wikipedia/commons/0/03/Flag_of_Italy.svg");
        public static Cuisine French => new(nameof(French), "https://upload.wikimedia.org/wikipedia/commons/c/c3/Flag_of_France.svg");
        public static Cuisine Chinese => new(nameof(Chinese), "https://upload.wikimedia.org/wikipedia/commons/f/fa/Flag_of_the_People%27s_Republic_of_China.svg");

        public static IEnumerable<Cuisine> GetAll()
            => new List<Cuisine>
            {
                Italian, French, Chinese, Danish
            };
    }
}
