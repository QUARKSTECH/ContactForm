using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContactForm.API.Helpers;

namespace ContactForm.API.Data
{
    public abstract class HelperFields
    {
        public string ExtraProperties { get; set; }

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