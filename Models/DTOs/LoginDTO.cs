﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class LoginDTO
    {
        public required string Correo { get; set; }
        public required string Clave { get; set; }
    }
}
