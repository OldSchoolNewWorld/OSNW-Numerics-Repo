Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Runtime.CompilerServices
Imports System.Numerics

Public Class ComplexStandardFormatter
    Implements IFormatProvider, ICustomFormatter

    '   REF: Format a complex number
    ' https://learn.microsoft.com/en-us/dotnet/fundamentals/runtime-libraries/system-numerics-complex#format-a-complex-number

    Private Const CHARI As System.Char = "i"c
    Private Const CHARMINUS As System.Char = "-"c
    Private Const CHARPLUS As System.Char = "+"c

    Public Function GetFormat(formatType As Type) As Object _
        Implements IFormatProvider.GetFormat

        If formatType Is GetType(ICustomFormatter) Then
            Return Me
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Implements the <see cref="ICustomFormatter.Format"/> method to format
    ''' a <see cref="System.Numerics.Complex"/> number in one of several
    ''' standard forms. The format string can be one of the following:
    ''' <list type="bullet">
    ''' <item><term>AIBC</term>
    ''' <description>Closed form of A+iB.</description></item>
    ''' <item><term>AIBO</term>
    ''' <description>Open form of A + iB.</description></item>
    ''' <item><term>ABIC</term>
    ''' <description>Open form of A + Bi.</description></item>
    ''' <item><term>ABIO</term>
    ''' <description>Closed form of A+Bi.</description></item>
    ''' </list>
    ''' The format string can also include a precision specifier, e.g. "AIBC3".
    ''' </summary>
    ''' <param name="fmt">xxxxxxxxxxxxxxxxxxxxxx</param>
    ''' <param name="arg">xxxxxxxxxxxxxxxxxxxxxx</param>
    ''' <param name="provider">xxxxxxxxxxxxxxxxxxxxxx</param>
    ''' <returns>xxxxxxxxxxxxxxxxxxxxxx</returns>
    Public Function Format(ByVal fmt As System.String,
                           ByVal arg As System.Object,
                           ByVal provider As IFormatProvider) _
        As System.String _
        Implements ICustomFormatter.Format

        If TypeOf arg Is System.Numerics.Complex Then
            Dim c1 As System.Numerics.Complex =
                DirectCast(arg, System.Numerics.Complex)

            ' Check if the format string has a precision specifier.
            Dim Precision As System.Int32
            Dim FmtString As System.String = System.String.Empty
            If fmt.Length > 4 Then
                Try
                    Precision = System.Int32.Parse(fmt.Substring(4))
                Catch e As System.FormatException
                    Precision = 0
                End Try
                FmtString = "N" + Precision.ToString()
            End If
            Dim Sign As System.Char =
                If(c1.Imaginary < 0.0, CHARMINUS, CHARPLUS)

            Select Case True
                Case fmt.Substring(0, 4).Equals("AIBC",
                    System.StringComparison.OrdinalIgnoreCase)

                    Return c1.Real.ToString(FmtString) + $"{Sign}{CHARI}" +
                        Math.Abs(c1.Imaginary).ToString(FmtString)
                Case fmt.Substring(0, 4).Equals("AIBO",
                    System.StringComparison.OrdinalIgnoreCase)

                    Return c1.Real.ToString(FmtString) + $" {Sign} {CHARI}" +
                        Math.Abs(c1.Imaginary).ToString(FmtString)
                Case fmt.Substring(0, 4).Equals("ABIC",
                    System.StringComparison.OrdinalIgnoreCase)

                    Return c1.Real.ToString(FmtString) + Sign +
                        Math.Abs(c1.Imaginary).ToString(FmtString) + CHARI
                Case fmt.Substring(0, 4).Equals("ABIO",
                    System.StringComparison.OrdinalIgnoreCase)

                    Return c1.Real.ToString(FmtString) + $" {Sign} " +
                        Math.Abs(c1.Imaginary).ToString(FmtString) + CHARI
                Case Else
                    Return c1.ToString(fmt, provider)
            End Select

        Else
            If TypeOf arg Is System.IFormattable Then
                Return DirectCast(
                    arg, System.IFormattable).ToString(fmt, provider)
            ElseIf arg IsNot Nothing Then
                Return arg.ToString()
            Else
                Return String.Empty
            End If
        End If
    End Function

End Class ' ComplexStandardFormatter

''' <summary>
''' This module contains extension methods for the
''' <see cref="System.Numerics.Complex"/> structure.
''' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
''' </summary>
''' <remarks>
''' The extension methods are used to convert a <c>Complex</c> to its equivalent
''' string representation in the standard form of A + iB or A + Bi.
''' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
''' </remarks>
Public Module Complex

    Private Const CHARI As System.Char = "i"c
    Private Const CHARMINUS As System.Char = "-"c
    Private Const CHARPLUS As System.Char = "+"c
    Private Const CHARUPPERE As System.Char = "E"c

#Region "ToString"

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the standard form of A+iB.
    ''' </summary>
    ''' <returns>The A+iB standard form representation of the current
    ''' <c>Complex</c>.</returns>
    <Extension()>
    Public Function ToStringAIB(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{CHARI}{Math.Abs(aComplex.Imaginary)}"
    End Function ' ToStringAIB

    ''' <summary>
    ''' Converts the value of a <see cref="System.Numerics.Complex"/> to its
    ''' equivalent string representation in the standard form of A+Bi.
    ''' </summary>
    ''' <returns>The A+Bi standard form representation of the current
    ''' <c>Complex</c>.</returns>
    <Extension()>
    Public Function ToStringABI(ByVal aComplex As System.Numerics.Complex) _
        As System.String

        Dim Sign As System.String =
            If(aComplex.Imaginary < 0.0, CHARMINUS, CHARPLUS)
        Return $"{aComplex.Real}{Sign}{Math.Abs(aComplex.Imaginary)}{CHARI}"
    End Function ' ToStringABI

#End Region ' "ToString"

End Module ' Complex
