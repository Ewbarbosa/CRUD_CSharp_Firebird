﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Crud_Firebird
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }
    }
}
