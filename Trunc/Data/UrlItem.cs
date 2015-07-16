using System;
using Biggy.Core;

namespace Trunc.Data {
    public class UrlItem : IEquatable<UrlItem> {
        private string _customUrl;

        public UrlItem() {
            CreatedOn = DateTime.Now;
        }

        [PrimaryKey(true)]
        public int Id { get; set; }

        public string CustomUrl {
            get { return string.IsNullOrWhiteSpace(_customUrl) ? UrlGenerator.Encode(Id) : _customUrl; }
            set { _customUrl = value; }
        }

        public string OriginUrl { get; set; }

        public double ExpireInDays { get; set; }

        /// <summary>
        ///     Kept as short as Biggy can't implicitly cast enums to integers
        /// </summary>
        public short ExpireMode { get; set; }

        public DateTime CreatedOn { get; set; }

        #region IEquatable<UrlItem> Members

        public bool Equals(UrlItem other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Id == other.Id && string.Equals(OriginUrl, other.OriginUrl) && ExpireInDays.Equals(other.ExpireInDays) && ExpireMode == other.ExpireMode &&
                   CreatedOn.Equals(other.CreatedOn);
        }

        #endregion

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((UrlItem) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = Id;
                hashCode = (hashCode * 397) ^ (OriginUrl != null ? OriginUrl.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ExpireInDays.GetHashCode();
                hashCode = (hashCode * 397) ^ ExpireMode.GetHashCode();
                hashCode = (hashCode * 397) ^ CreatedOn.GetHashCode();
                return hashCode;
            }
        }
    }
}
