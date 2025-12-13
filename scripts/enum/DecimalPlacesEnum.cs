using Godot;
using System;
using System.ComponentModel;

namespace BoldBet.Enum;
public enum DecimalPlacesEnum
{
	Zero,
	One,
	Two,	
}


public static class FormatDecimalPlaces
{
	public static string Formater(float valeur, DecimalPlacesEnum precision)
	{
		return valeur.ToString($"F{(int)precision}");
	}
}