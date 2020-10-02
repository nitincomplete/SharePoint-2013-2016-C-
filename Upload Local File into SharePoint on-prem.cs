        private static void UploadDoc()
        {
            string fileName = "myLocalFile.txt";
            string sourceUrl = @"C:\Users\XXXXX\Downloads\" + fileName;

            string destinationURL = @"https://test-sharepoint.mydomain.com/site1/Shared%20Documents/"+ fileName;
            string[] destinationUrlArray = new string[] { destinationURL };

            byte[] postedByteArray = File.ReadAllBytes(sourceUrl);

            wsCopy.CopySoapClient wsCopyClient = new CopySoapClient(); //https://test-sharepoint.mydomain.com/_vti_bin/Copy.asmx

            wsCopy.FieldInformation i1 = new wsCopy.FieldInformation { DisplayName = "Title", InternalName = "Title", Type = wsCopy.FieldType.Text };
            wsCopy.FieldInformation[] info = { i1 };
            CopyResult[] resultsArray;

            // Upload the document to the SharePoint document library
            wsCopyClient.CopyIntoItems(sourceUrl, destinationUrlArray, info, postedByteArray, out resultsArray);

            if (resultsArray[0].ErrorCode != wsCopy.CopyErrorCode.Success)
            {
                Console.WriteLine("Error occured during document upload process.");
                throw new Exception("Error Occured!");
            }
        }
        
        //App.config or web.config
        <system.serviceModel>
          <bindings>
            <basicHttpBinding>
              <binding name="CopySoap" maxBufferPoolSize="2147483647"
              maxReceivedMessageSize="2147483647"
              maxBufferSize="2147483647" transferMode="Buffered">
                <security mode="Transport" >
                  <transport clientCredentialType="Ntlm" proxyCredentialType="Ntlm" realm="" />
                  <message clientCredentialType="UserName" algorithmSuite="Default" />
                </security>
              </binding>
              <binding name="CopySoap1" maxBufferPoolSize="2147483647"
              maxReceivedMessageSize="2147483647"
              maxBufferSize="2147483647" transferMode="Buffered" />
            </basicHttpBinding>
          </bindings>
          <client>
            <endpoint address="https://test-sharepoint.mydomain.com/_vti_bin/Copy.asmx"
              binding="basicHttpBinding" bindingConfiguration="CopySoap" contract="wsCopy.CopySoap"
              name="CopySoap"  />
          </client>
        </system.serviceModel>
