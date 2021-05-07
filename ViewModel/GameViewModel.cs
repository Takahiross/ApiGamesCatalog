using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.ViewModel
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Producer { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }

    }
}
