namespace SparkPars {
    internal class Page {
        private string _nameCategory;
        public string NameCategory {
            get { return _nameCategory; }
            set { _nameCategory = value; }
        }

        private string _urlCategory;
        public string UrlCategory {
            get { return _urlCategory; }
            set { _urlCategory = value; }
        }

        private int _countPage;
        public int CountPage {
            get { return _countPage; }
            set { _countPage = value; }
        }

        public Page(string nameCategory,string urlCategory) {
            NameCategory = nameCategory;
            UrlCategory = urlCategory;
        }

        public override string ToString() {
            return $"{NameCategory} - {UrlCategory} \nPages: {CountPage}";
        }
    }
}
