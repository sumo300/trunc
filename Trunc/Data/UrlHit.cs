using System;
using Biggy.Core;

namespace Trunc.Data {
    public class UrlHit : IEquatable<UrlHit> {
        public UrlHit() {
            HitOn = DateTime.Now;
        }

        [PrimaryKey(true)]
        public int Id { get; set; }

        public int UrlItemId { get; set; }

        public string ClientIp { get; set; }

        public DateTime HitOn { get; private set; }

        #region IEquatable<UrlHit> Members

        public bool Equals(UrlHit other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return Id == other.Id && UrlItemId == other.UrlItemId && string.Equals(ClientIp, other.ClientIp) && HitOn.Equals(other.HitOn);
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
            return Equals((UrlHit) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = Id;
                hashCode = (hashCode * 397) ^ UrlItemId;
                hashCode = (hashCode * 397) ^ (ClientIp != null ? ClientIp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ HitOn.GetHashCode();
                return hashCode;
            }
        }
    }
}
