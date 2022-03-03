// PbapClient 
// Phone Book Access Profile client

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using MixERP.Net.VCards;

// MyPhone.OBEX namespace
namespace MyPhone.OBEX
{

   
    // PbapClient class 
    public class PbapClient : ObexClient
    {
        // PbapClient
        public PbapClient(IInputStream inputStream, IOutputStream outputStream) 
            : base(inputStream, outputStream)
        {
            
        }//PbapClient end

        /// <summary>
        /// Retrieves an phone book object from the object exchange server.
        /// </summary>
        /// <param name="phoneBookObjectPath">
        /// Absolute path in the virtual folders architecture of the PSE, 
        /// appended with the name of the file representation of one of the Phone Book Objects.
        /// Example: telecom/pb.vcf or SIM1/telecom/pb.vcf for the main phone book objects
        /// </param>
        /// <returns>phone book object string</returns>
        // PullPhoneBook
        public async Task<string> PullPhoneBook(string phoneBookObjectPath)
        {
            ObexPacket request = new ObexPacket(Opcode.GetAlter);
            
            request.Headers[HeaderId.Name] = new UnicodeStringValueHeader(HeaderId.Name, phoneBookObjectPath);
            request.Headers[HeaderId.Type] = new AsciiStringValueHeader(HeaderId.Type, "x-bt/phonebook");
            request.Headers[HeaderId.ApplicationParameters] = new AppParamHeader();
            
            ObexPacket response = await RunObexRequest(request);

            Console.WriteLine(((BodyHeader)response.Headers[HeaderId.Body]).Value);

            return ((BodyHeader)response.Headers[HeaderId.Body]).Value!;
        
        }//PullPhoneBook end


        // GetAllContacts
        public async Task<IEnumerable<VCard>> GetAllContacts()
        {
            string str = await PullPhoneBook("telecom/pb.vcf");

            return Deserializer.GetVCards(str);
        }//GetAllContacts end


        // GetCombinedCallHistory
        public async Task<IEnumerable<VCard>> GetCombinedCallHistory()
        {
            string str = await PullPhoneBook("telecom/cch.vcf");

            return Deserializer.GetVCards(str);
        }//GetCombinedCallHistory end


        // GetIncomingCallHistory
        public async Task<IEnumerable<VCard>> GetIncomingCallHistory()
        {
            string str = await PullPhoneBook("telecom/ich.vcf");
       
            return Deserializer.GetVCards(str);
        }//GetIncomingCallHistory end 

        // GetOutgoingCallHistory
        public async Task<IEnumerable<VCard>> GetOutgoingCallHistory()
        {
            string str = await PullPhoneBook("telecom/och.vcf");

            return Deserializer.GetVCards(str);
        }//GetOutgoingCallHistory end


        // GetMissedCallHistory
        public async Task<IEnumerable<VCard>> GetMissedCallHistory()
        {
            string str = await PullPhoneBook("telecom/mch.vcf");

            return Deserializer.GetVCards(str);

        }//GetMissedCallHistory end


        // GetSpeedDial
        public async Task<IEnumerable<VCard>> GetSpeedDial()
        {
            string str = await PullPhoneBook("telecom/spd.vcf");

            return Deserializer.GetVCards(str);

        }//GetSpeedDial end

    }//PbapClient class end

}//MyPhone.OBEX namespace end
