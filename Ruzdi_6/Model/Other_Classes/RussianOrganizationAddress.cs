﻿using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Ruzdi_6.Model.Other_Classes
{
    public class RussianOrganizationAddress : IDataErrorInfo
    {
        public RussianOrganizationAddress()
        {
        }
        private string region;
        private string district;
        private string city;
        private string locality;
        private string street;
        private string house;
        private string building;
        private string apartment;
        string pattern60 = @"^[\w \s \W]{1,60}$";
        string pattern8 = @"^[\w \s \W]{1,8}$";
        public string RegionCode { get; set; }
        public string Region
        {
            get => region;
            set
            {
                region = value;
                errors["Region"] = (Region is null || Region.Length == 0) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string District
        {
            get => district;
            set
            {
                district = value;
                errors["District"] = (!Regex.IsMatch(District, pattern60) && District.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string City
        {
            get => city;
            set
            {
                city = value;
                errors["City"] = (!Regex.IsMatch(City, pattern60) && City.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string Locality
        {
            get => locality;
            set
            {
                locality = value;
                errors["Locality"] = (!Regex.IsMatch(Locality, pattern60) && Locality.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string Street
        {
            get => street;
            set
            {
                street = value;
                errors["Street"] = (!Regex.IsMatch(Street, pattern60) && Street.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string House
        {
            get => house;
            set
            {
                house = value;
                errors["House"] = (!Regex.IsMatch(House, pattern8) && House.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string Building
        {
            get => building;
            set
            {
                building = value;
                errors["Building"] = (!Regex.IsMatch(Building, pattern8) && Building.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }
        public string Apartment
        {
            get => apartment;
            set
            {
                apartment = value;
                errors["Apartment"] = (!Regex.IsMatch(Apartment, pattern8) && Apartment.Length > 0) ? "Текст сообщения об ошибке" : null;
            }
        }

        public string Error => throw new System.NotImplementedException();
        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        #region Методы для сериализатора
        public bool ShouldSerializeDistrict() => District?.Length > 0;
        public bool ShouldSerializeCity() => City?.Length > 0;
        public bool ShouldSerializeLocality() => Locality?.Length > 0;
        public bool ShouldSerializeStreet() => Street?.Length > 0;
        public bool ShouldSerializeHouse() => House?.Length > 0;
        public bool ShouldSerializeBuilding() => Building?.Length > 0;
        public bool ShouldSerializeApartment() => Apartment?.Length > 0;

        #endregion
    }
}
