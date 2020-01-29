using System;
using System.Collections.Generic;


namespace SecretSanta.Data
{
	public class Group : FingerPrintEntityBase
	{
		#region Fields
		private string _Title = string.Empty;
		#endregion

		#region Properties
#nullable disable
		public string Title { get => _Title; set => _Title = value ?? throw new ArgumentNullException(nameof(Title)); }
		public List<UserGroup> UserGroup { get; } = new List<UserGroup>();
#nullable enable
		#endregion

		#region Constructor
		//public Group(string title)
		//{
		//    Title = title ?? throw new ArgumentNullException(nameof(title)); 
		//}
		#endregion
	}
}
