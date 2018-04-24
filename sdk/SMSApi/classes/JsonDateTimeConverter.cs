using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
#if !NET20
using System.Linq;
#endif
using System.Text;

namespace InteractuaMovil.ContactoSms.Api
{
    public class JsonDateTimeConverter : IsoDateTimeConverter
    {
        private const String format = "yyyy'-'MM'-'dd HH':'mm':'ss";

        public JsonDateTimeConverter()
        {
            this.DateTimeFormat = format;
        }
        
    }
}
