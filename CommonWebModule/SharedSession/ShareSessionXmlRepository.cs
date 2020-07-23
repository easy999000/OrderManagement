using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CommonWebModule.SharedSession
{
    public class ShareSessionXmlRepository : IXmlRepository
    {
        private string key = @"<?xml version='1.0' encoding='utf-8'?>
<key id='58df9eaf-abcc-4637-8a47-e9ce41a65543' version='1'>
  <creationDate>2020-06-12T23:52:11.2254149Z</creationDate>
  <activationDate>2020-06-12T23:52:11.1874984Z</activationDate>
  <expirationDate>2070-09-10T23:52:11.1874984Z</expirationDate>
  <descriptor deserializerType='Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=3.1.5.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'>
    <descriptor>
      <encryption algorithm='AES_256_CBC' />
      <validation algorithm='HMACSHA256' />
      <masterKey p4:requiresEncryption='true' xmlns:p4='http://schemas.asp.net/2015/03/dataProtection'>
        <!-- Warning: the key below is in an unencrypted form. -->
        <value>a7ka4ZHOrC+1AIbZLfSD7f9sOkUT5rJqNy+KvlwG8CWXX3KctoDlGVqjrCdO0HIOODKtYd2ZZkLpNYZS+jIn8Q==</value>
      </masterKey>
    </descriptor>
  </descriptor>
</key>";

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            //XmlDocument xml = new XmlDocument();
            //xml.LoadXml(key);


            //return xml.ReadNode();

            return GetAllElementsCore().ToList().AsReadOnly();
        }
        private IEnumerable<XElement> GetAllElementsCore()
        {
            yield return XElement.Parse(key);
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            if (element == null)
            {
                return;
            }
            StoreElementCore(element, friendlyName);
        }

        private void StoreElementCore(XElement element, string filename)
        {
        }
    }
}
