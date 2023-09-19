using Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.ViewModels;

public class OrderGetVM
{
    public int Id { get; set; }

    public PaymentType PaymentType { get; set; }

    public bool IsCompleted { get; set; }

    public double TotalPrice { get; set; }
}
