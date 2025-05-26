Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

''' <summary>
''' An electrical Impedance Z is a number of the standard form Z=R+jX or R+Xj,
''' where R and X are real numbers, and j is the imaginary unit, with the
''' property j^2 = -1.
''' </summary>
Public Structure Impedance

    ' DEV: An Impedance is essentially a complex number with some cosmetic
    ' differences:
    '   'i' is replaced by 'j' in the standard form.
    '   The Real component is represented by Resistance.
    '   The Imaginary component is represented by Reactance.
    ' Since System.Numerics.Complex is represented as a structure, it cannot be
    ' inherited. Given that, Impedance is created as a structure which uses
    ' familiar terminology but relies on Complex for most of its work.

    Private Const CHARI As System.Char = "i"c
    Private Const CHARJ As System.Char = "j"c
    Private Const CHARMINUS As System.Char = "-"c
    Private Const CHARPLUS As System.Char = "+"c
    Private Const CHARUPPERE As System.Char = "E"c

    ' Use the "has a ..." approach to expose the desired features of a
    ' System.Numerics.Complex.
    ' Do not rename (binary serialization).
    Private ReadOnly m_Complex As System.Numerics.Complex

    ''' <summary>
    ''' Gets the resistive component of the current
    ''' <c>OSNW.Numerics.Impedance</c> object.   
    ''' </summary>
    ''' <returns>The resistive component of an Impedance.</returns>
    Public ReadOnly Property Resistance As System.Double
        Get
            Return Me.m_Complex.Real
        End Get
    End Property

    ''' <summary>
    ''' Gets the reactive component of the current
    ''' <c>OSNW.Numerics.Impedance</c> object.   
    ''' </summary>
    ''' <returns>The reactive component of an Impedance.</returns>
    Public ReadOnly Property Reactance As System.Double
        Get
            Return Me.m_Complex.Imaginary
        End Get
    End Property

#Region "ToString"

    ''' <summary>
    ''' Converts the value of the current complex number to its equivalent
    ''' string representation in Cartesian form.
    ''' </summary>
    ''' <returns>The string representation of the current instance in Cartesian
    ''' form.</returns>
    Public Overrides Function ToString() As System.String
        Return Me.m_Complex.ToString
    End Function ' ToString

    ''' <summary>
    ''' Converts the value of a <see cref="OSNW.Numerics.Impedance"/> to its
    ''' equivalent string representation in the standard form of R+jX.
    ''' </summary>
    ''' <returns>The R+jX standard form representation of the current
    ''' <c>Impedance</c>.</returns>
    Public Function ToStringRjX() As System.String
        Return Me.m_Complex.ToStringAIB.Replace(CHARI, CHARJ)
    End Function ' ToStringRjX

    ''' <summary>
    ''' Converts the value of a <see cref="OSNW.Numerics.Impedance"/> to its
    ''' equivalent string representation in the standard form of R+Xj.
    ''' </summary>
    ''' <returns>The R+Xj standard form representation of the current
    ''' <c>Impedance</c>.</returns>
    Public Function ToStringRXj() As System.String
        Return Me.m_Complex.ToStringABI.Replace(CHARI, CHARJ)
    End Function ' ToStringRXj

#End Region ' "ToString"

#Region "Constructors"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="OSNW.Numerics.Impedance"/>
    ''' structure with the specified resistance and reactance values.
    ''' </summary>
    ''' <param name="resistance"></param>
    ''' <param name="reactance"></param>
    Public Sub New(ByVal resistance As System.Double, ByVal reactance As System.Double)
        Me.m_Complex = New System.Numerics.Complex(resistance, reactance)
    End Sub ' New

#End Region ' "Constructors"

End Structure ' Impedance
