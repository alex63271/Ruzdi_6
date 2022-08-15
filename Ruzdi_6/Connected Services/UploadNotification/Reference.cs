﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторного создания кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UploadNotification
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0", ConfigurationName="UploadNotification.ruzdiUploadNotificationPackageService_v1_1PortType")]
    public interface ruzdiUploadNotificationPackageService_v1_1PortType
    {
        
        // CODEGEN: Создается контракт сообщения, так как операция имеет много возвращаемых значений.
        [System.ServiceModel.OperationContractAttribute(Action="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService", ReplyAction="//fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0/uploadNotificationPac" +
            "kageService/uploadNotificationPackageResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<UploadNotification.uploadNotificationPackageResponse> uploadNotificationPackageAsync(UploadNotification.uploadNotificationPackageRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0")]
    public partial class pledgeNotificationPackageType
    {
        
        private string packageIdField;
        
        private string uipField;
        
        private senderTypeType senderTypeField;
        
        private pledgeNotificationListElementType[] pledgeNotificationListField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string packageId
        {
            get
            {
                return this.packageIdField;
            }
            set
            {
                this.packageIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string uip
        {
            get
            {
                return this.uipField;
            }
            set
            {
                this.uipField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public senderTypeType senderType
        {
            get
            {
                return this.senderTypeField;
            }
            set
            {
                this.senderTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("pledgeNotificationListElement", IsNullable=false)]
        public pledgeNotificationListElementType[] pledgeNotificationList
        {
            get
            {
                return this.pledgeNotificationListField;
            }
            set
            {
                this.pledgeNotificationListField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0")]
    public enum senderTypeType
    {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0")]
    public partial class pledgeNotificationListElementType
    {
        
        private string notificationIdField;
        
        private byte[] documentAndSignatureField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string notificationId
        {
            get
            {
                return this.notificationIdField;
            }
            set
            {
                this.notificationIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", Order=1)]
        public byte[] documentAndSignature
        {
            get
            {
                return this.documentAndSignatureField;
            }
            set
            {
                this.documentAndSignatureField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0")]
    public partial class StateType
    {
        
        private string codeField;
        
        private string messageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="uploadNotificationPackageRequest", WrapperNamespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0", IsWrapped=true)]
    public partial class uploadNotificationPackageRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0", Order=0)]
        public UploadNotification.pledgeNotificationPackageType pledgeNotificationPackage;
        
        public uploadNotificationPackageRequest()
        {
        }
        
        public uploadNotificationPackageRequest(UploadNotification.pledgeNotificationPackageType pledgeNotificationPackage)
        {
            this.pledgeNotificationPackage = pledgeNotificationPackage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="uploadNotificationPackageResponse", WrapperNamespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0", IsWrapped=true)]
    public partial class uploadNotificationPackageResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0", Order=0)]
        public string registrationId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://fciit.ru/eis2/ruzdi/uploadNotificationPackageService/v1_0", Order=1)]
        public UploadNotification.StateType packageStateCode;
        
        public uploadNotificationPackageResponse()
        {
        }
        
        public uploadNotificationPackageResponse(string registrationId, UploadNotification.StateType packageStateCode)
        {
            this.registrationId = registrationId;
            this.packageStateCode = packageStateCode;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface ruzdiUploadNotificationPackageService_v1_1PortTypeChannel : UploadNotification.ruzdiUploadNotificationPackageService_v1_1PortType, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class ruzdiUploadNotificationPackageService_v1_1PortTypeClient : System.ServiceModel.ClientBase<UploadNotification.ruzdiUploadNotificationPackageService_v1_1PortType>, UploadNotification.ruzdiUploadNotificationPackageService_v1_1PortType
    {
        
        /// <summary>
        /// Реализуйте этот разделяемый метод для настройки конечной точки службы.
        /// </summary>
        /// <param name="serviceEndpoint">Настраиваемая конечная точка</param>
        /// <param name="clientCredentials">Учетные данные клиента.</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ruzdiUploadNotificationPackageService_v1_1PortTypeClient(EndpointConfiguration endpointConfiguration) : 
                base(ruzdiUploadNotificationPackageService_v1_1PortTypeClient.GetBindingForEndpoint(endpointConfiguration), ruzdiUploadNotificationPackageService_v1_1PortTypeClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ruzdiUploadNotificationPackageService_v1_1PortTypeClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ruzdiUploadNotificationPackageService_v1_1PortTypeClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ruzdiUploadNotificationPackageService_v1_1PortTypeClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ruzdiUploadNotificationPackageService_v1_1PortTypeClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ruzdiUploadNotificationPackageService_v1_1PortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<UploadNotification.uploadNotificationPackageResponse> uploadNotificationPackageAsync(UploadNotification.uploadNotificationPackageRequest request)
        {
            return base.Channel.uploadNotificationPackageAsync(request);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.ruzdiUploadNotificationPackageService_v1_1HttpSoap12Endpoint))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint))
            {
                return new System.ServiceModel.EndpointAddress("http://ruzditest.eisnot.ru:8280/services/ruzdiUploadNotificationPackageService_v1" +
                        "_1.ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint");
            }
            if ((endpointConfiguration == EndpointConfiguration.ruzdiUploadNotificationPackageService_v1_1HttpSoap12Endpoint))
            {
                return new System.ServiceModel.EndpointAddress("http://ruzditest.eisnot.ru:8280/services/ruzdiUploadNotificationPackageService_v1" +
                        "_1.ruzdiUploadNotificationPackageService_v1_1HttpSoap12Endpoint");
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            ruzdiUploadNotificationPackageService_v1_1HttpSoap11Endpoint,
            
            ruzdiUploadNotificationPackageService_v1_1HttpSoap12Endpoint,
        }
    }
}
