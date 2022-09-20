using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;


namespace Ruzdi_6.Services
{
    public static class EisCmsSigner
    {
        public static byte[] Sign(byte[] keyBytes)
        {
            // Получаем сертификат ключа подписи.
            // Он будет использоваться для получения секретного ключа подписи.
            //X509Certificate2 signerCert = CertificateSelector.SelectBySerialNumber("51b4970076acb9bd48d4709fe0d0451f");
            X509Certificate2 signerCert = SelectWithUi();

            byte[] encodedSignature = SignMsg(keyBytes, signerCert);

            return encodedSignature;
        }


        // Подписываем сообщение секретным ключом.
        private static byte[] SignMsg(byte[] msg, X509Certificate2 signerCert)
        {
            // Создаем объект ContentInfo по сообщению. Это необходимо для создания объекта SignedCms.
            var contentInfo = new ContentInfo(msg);

            // Создаем объект SignedCms по только что созданному объекту ContentInfo.
            // SubjectIdentifierType установлен по умолчанию в IssuerAndSerialNumber.
            // Свойство Detached устанавливаем явно в true, таким образом сообщение будет отделено от подписи.
            var signedCms = new SignedCms(contentInfo, true);           

            // Определяем подписывающего, объектом CmsSigner.
            var cmsSigner = new CmsSigner(signerCert);

            // Добавляем подписываемый штамп времени в 
            // подписываемые атрибуты.
            // В качестве времени проставляем текущее время.
            cmsSigner.SignedAttributes.Add(new Pkcs9SigningTime(DateTime.UtcNow));
            


            // Подписываем CMS/PKCS #7 сообщение.
            signedCms.ComputeSignature(cmsSigner, false);

            // Кодируем CMS/PKCS #7 подпись сообщения.
            return signedCms.Encode();
        }

        private static X509Certificate2 SelectWithUi()
        {
            // Формируем коллекцию отображаемых сертификатов.
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            try
            {
                var userCertificates = store.Certificates.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, true);

                // Отображаем окно выбора сертификата.
                var title = "Выбор секретного ключа по сертификату";
                var message = "Выберите сертификат соответствующий Вашему секретному ключу.";
                var selectedCertificates = X509Certificate2UI.SelectFromCollection(userCertificates, title, message, X509SelectionFlag.SingleSelection);

                // Проверяем, что выбран сертификат
                if (selectedCertificates.Count == 0)
                {
                    return null;
                }

                // Выбран может быть только один сертификат.
                var foundCert = selectedCertificates[0];

                return foundCert;
            }
            finally
            {
                store.Close();
                store.Dispose();
            }
        }

        public static X509Certificate2 SelectBySerialNumber(string certSerialNumber)
        {
            // Открываем хранилище My.
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            store.Open(OpenFlags.ReadOnly);

            try
            {
                // Ищем сертификат для подписи.
                var certColl = store.Certificates.Find(X509FindType.FindBySerialNumber, certSerialNumber, true);

                // Проверяем, что нашли требуемый сертификат
                if (certColl.Count == 0)
                {
                    return null;
                }

                // Если найдено более одного сертификата, возвращаем первый попавшийся.
                return certColl[0];
            }
            finally
            {
                store.Close();
                store.Dispose();
            }
        }
    }
}
