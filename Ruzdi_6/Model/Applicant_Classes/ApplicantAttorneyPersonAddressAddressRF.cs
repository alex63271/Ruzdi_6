﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ruzdi_6.Model.Applicant_Classes
{
    public class ApplicantAttorneyPersonAddressAddressRF : IDataErrorInfo
    {
        public ApplicantAttorneyPersonAddressAddressRF()
        {
        }
        public string RegionCode { get; set; }
        public string Region
        {
            get => region;
            set
            {
                region = value;
                errors["Region"] = string.IsNullOrEmpty(Region) ? "Текст сообщения об ошибке" : null;
            }
        }

        [XmlAttribute]
        public bool registration { get; set; }


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
        public string Error => throw new NotImplementedException();

        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public bool IsValid => !errors.Values.Any(x => x != null);

        Dictionary<string, string> errors = new Dictionary<string, string>();
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

        #region Методы для сериализатора
        public bool ShouldSerializeDistrict() => !string.IsNullOrEmpty(District);
        public bool ShouldSerializeCity() => !string.IsNullOrEmpty(City);
        public bool ShouldSerializeLocality() => !string.IsNullOrEmpty(Locality);
        public bool ShouldSerializeStreet() => !string.IsNullOrEmpty(Street);
        public bool ShouldSerializeHouse() => !string.IsNullOrEmpty(House);
        public bool ShouldSerializeBuilding() => !string.IsNullOrEmpty(Building);
        public bool ShouldSerializeApartment() => !string.IsNullOrEmpty(Apartment);

        #endregion
    }
}
