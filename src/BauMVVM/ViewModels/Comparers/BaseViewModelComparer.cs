using System;
using System.Collections.Generic;

using Bau.Libraries.BauMvvm.ViewModels;

namespace Bau.Libraries.MVVM.ViewModels.Comparer
{
	/// <summary>
	///		Clase base para los comparadores
	/// </summary>
	public abstract class BaseViewModelComparer<TypeData> : IComparer<TypeData> where TypeData : BaseObservableObject
	{
		public BaseViewModelComparer(bool ascending)
		{
			IsAscending = ascending;
		}

		/// <summary>
		///		Compara dos elementos
		/// </summary>
		public int Compare(TypeData x, TypeData y)
		{
			if (x == null && y == null)
				return 0;
			else
				return GetSortFactor() * CompareData(x, y);
		}

		/// <summary>
		///		Compara dos elementos
		/// </summary>
		protected abstract int CompareData(TypeData first, TypeData second);

		/// <summary>
		///		Factor para la ordenación ascendente / descendente
		/// </summary>
		private int GetSortFactor()
		{
			if (IsAscending)
				return 1;
			else
				return -1;
		}

		/// <summary>
		///		Indica si la ordenación es ascendente / descendente
		/// </summary>
		private bool IsAscending { get; set; }
	}
}
