namespace RGC.Task.ShipStation.OrderGet.Dto
{
    /// <summary>
    /// shane 2020/4/21 11:17:05
    /// </summary>
    public class ShipNoticeDto
    {
        private string orderNumberField;

        private uint orderIDField;

        private string customerCodeField;

        private object customerNotesField;

        private object internalNotesField;

        private object notesToCustomerField;

        private object notifyCustomerField;

        private string labelCreateDateField;

        private string shipDateField;

        private string carrierField;

        private string serviceField;

        private string trackingNumberField;

        private decimal shippingCostField;

        private object customField1Field;

        private object customField2Field;

        private object customField3Field;

        private ShipNoticeRecipient recipientField;

        private ShipNoticeItems itemsField;

        /// <remarks/>
        public string OrderNumber
        {
            get
            {
                return this.orderNumberField;
            }
            set
            {
                this.orderNumberField = value;
            }
        }

        /// <remarks/>
        public uint OrderID
        {
            get
            {
                return this.orderIDField;
            }
            set
            {
                this.orderIDField = value;
            }
        }

        /// <remarks/>
        public string CustomerCode
        {
            get
            {
                return this.customerCodeField;
            }
            set
            {
                this.customerCodeField = value;
            }
        }

        /// <remarks/>
        public object CustomerNotes
        {
            get
            {
                return this.customerNotesField;
            }
            set
            {
                this.customerNotesField = value;
            }
        }

        /// <remarks/>
        public object InternalNotes
        {
            get
            {
                return this.internalNotesField;
            }
            set
            {
                this.internalNotesField = value;
            }
        }

        /// <remarks/>
        public object NotesToCustomer
        {
            get
            {
                return this.notesToCustomerField;
            }
            set
            {
                this.notesToCustomerField = value;
            }
        }

        /// <remarks/>
        public object NotifyCustomer
        {
            get
            {
                return this.notifyCustomerField;
            }
            set
            {
                this.notifyCustomerField = value;
            }
        }

        /// <remarks/>
        public string LabelCreateDate
        {
            get
            {
                return this.labelCreateDateField;
            }
            set
            {
                this.labelCreateDateField = value;
            }
        }

        /// <remarks/>
        public string ShipDate
        {
            get
            {
                return this.shipDateField;
            }
            set
            {
                this.shipDateField = value;
            }
        }

        /// <remarks/>
        public string Carrier
        {
            get
            {
                return this.carrierField;
            }
            set
            {
                this.carrierField = value;
            }
        }

        /// <remarks/>
        public string Service
        {
            get
            {
                return this.serviceField;
            }
            set
            {
                this.serviceField = value;
            }
        }

        /// <remarks/>
        public string TrackingNumber
        {
            get
            {
                return this.trackingNumberField;
            }
            set
            {
                this.trackingNumberField = value;
            }
        }

        /// <remarks/>
        public decimal ShippingCost
        {
            get
            {
                return this.shippingCostField;
            }
            set
            {
                this.shippingCostField = value;
            }
        }

        /// <remarks/>
        public object CustomField1
        {
            get
            {
                return this.customField1Field;
            }
            set
            {
                this.customField1Field = value;
            }
        }

        /// <remarks/>
        public object CustomField2
        {
            get
            {
                return this.customField2Field;
            }
            set
            {
                this.customField2Field = value;
            }
        }

        /// <remarks/>
        public object CustomField3
        {
            get
            {
                return this.customField3Field;
            }
            set
            {
                this.customField3Field = value;
            }
        }

        /// <remarks/>
        public ShipNoticeRecipient Recipient
        {
            get
            {
                return this.recipientField;
            }
            set
            {
                this.recipientField = value;
            }
        }

        /// <remarks/>
        public ShipNoticeItems Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }


    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ShipNoticeRecipient
    {

        private string nameField;

        private string companyField;

        private string address1Field;

        private object address2Field;

        private string cityField;

        private string stateField;

        private ushort postalCodeField;

        private string countryField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }

        /// <remarks/>
        public string Address1
        {
            get
            {
                return this.address1Field;
            }
            set
            {
                this.address1Field = value;
            }
        }

        /// <remarks/>
        public object Address2
        {
            get
            {
                return this.address2Field;
            }
            set
            {
                this.address2Field = value;
            }
        }

        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }

        /// <remarks/>
        public string State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public ushort PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ShipNoticeItems
    {

        private ShipNoticeItemsItem itemField;

        /// <remarks/>
        public ShipNoticeItemsItem Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ShipNoticeItemsItem
    {

        private string sKUField;

        private string nameField;

        private byte quantityField;

        private ushort lineItemIDField;

        /// <remarks/>
        public string SKU
        {
            get
            {
                return this.sKUField;
            }
            set
            {
                this.sKUField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public byte Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public ushort LineItemID
        {
            get
            {
                return this.lineItemIDField;
            }
            set
            {
                this.lineItemIDField = value;
            }
        }
    }
}
