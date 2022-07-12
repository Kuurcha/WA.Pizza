﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Table(nameof(RefreshToken))]
public class RefreshToken
{
    public string Token { get; set; }

    public DateTime ExpirationDate { get; set; }
}

