﻿using System;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate
{
    public class Address // ValueObject
    {
        public String Street { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public String ZipCode { get; set; }

        private Address() { }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street  = street;
            City    = city;
            State   = state;
            Country = country;
            ZipCode = zipcode;
        }
    }
}
