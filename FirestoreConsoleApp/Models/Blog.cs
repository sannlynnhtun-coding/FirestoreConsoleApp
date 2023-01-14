using Firestore.Interfaces;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireStoreConsoleApp.Models
{
    [FirestoreData]
    public class Blog : IBaseFirestoreEntity
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string BlogTitle { get; set; }
        [FirestoreProperty]
        public string BlogAuthor { get; set; }
        [FirestoreProperty]
        public string BlogContent { get; set; }
    }
}
