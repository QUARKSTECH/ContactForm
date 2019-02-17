using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContactForm.API.Helpers;

namespace ContactForm.API.Data
{
    public abstract class HelperFields
    {
        public HelperFields()
        {
            CreatedOn = DateTime.Now;
            ModifiedOn = DateTime.Now;
            IsActive = false;
        }
        public string ExtraProperties { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        private Dictionary<string, object> _extraProps {get; set;}

        [NotMapped]
        public Dictionary<string, object> ExtraProps
        {
            get
            {
                if(_extraProps == null)
                {
                    _extraProps = ExtraProperties.DeserializeDictionary();
                }
                return _extraProps;
            }
            set
            {
                _extraProps = value;
            }
        }
    }
}