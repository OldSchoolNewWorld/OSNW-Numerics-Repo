Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports Xunit
Imports OSNW.Numerics

Namespace Complex

    ' NOTE: Test values should be comprised of 1/2, 1/4, 1/8, ... to avoid
    ' failures due binary storage of fractional parts.

    ' Use this to run ad hoc tests.
    Public Class AdHoc
        '
        '
        '
        '
        '
    End Class

    Public Class General

        <Fact>
        Sub Constructor_WorksAsExpected()
            Dim Z As New System.Numerics.Complex(125.125, -50.5)
            Assert.Equal(125.125, Z.Real)
            Assert.Equal(-50.5, Z.Imaginary)
        End Sub

    End Class ' General

    Public Class ToString

        <Theory>
        <InlineData(125.125, 50.5, "<125.125; 50.5>")>
        <InlineData(-125.125, 50.5, "<-125.125; 50.5>")>
        <InlineData(125.125, -50.5, "<125.125; -50.5>")>
        <InlineData(-125.125, -50.5, "<-125.125; -50.5>")>
        <InlineData(Single.MaxValue, Single.MinValue, ' Encourage exponential notation.
                    "<3.4028234663852886E+38; -3.4028234663852886E+38>")>
        Sub ToString_Generic_WorksAsExpected(
            ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)

            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToString
            Assert.Equal(expected, Result)
        End Sub

        <Fact>
        Sub ToString_StandardForm_WorksAsExpected()

            Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Format AiBO0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBO0}", Cplx)}"
            Dim S2 As String = $"Format AiBC3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBC3}", Cplx)}"
            Dim S3 As String = $"Format ABiC0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiC0}", Cplx)}"
            Dim S4 As String = $"Format ABiO3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiO3}", Cplx)}"

            Dim Expect1 As String = "Format AiBO0: 12 - i15"
            Dim Expect2 As String = "Format AiBC3: 12.100-i15.400"
            Dim Expect3 As String = "Format ABiC0: 12-15i"
            Dim Expect4 As String = "Format ABiO3: 12.100 - 15.400i"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)
            Assert.Equal(Expect3, S3)
            Assert.Equal(Expect4, S4)

        End Sub

        <Fact>
        Sub ToStringAiBC_StandardForm_WorksAsExpected()

            Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Format AiBC0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBC0}", Cplx)}"
            Dim S2 As String = $"Format AiBC3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBC3}", Cplx)}"

            Dim Expect1 As String = "Format AiBC0: 12-i15"
            Dim Expect2 As String = "Format AiBC3: 12.100-i15.400"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)

        End Sub

        <Fact>
        Sub ToStringAiBO_StandardForm_WorksAsExpected()

            Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Format AiBO0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBO0}", Cplx)}"
            Dim S2 As String = $"Format AiBO3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:AiBO3}", Cplx)}"

            Dim Expect1 As String = "Format AiBO0: 12 - i15"
            Dim Expect2 As String = "Format AiBO3: 12.100 - i15.400"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)

        End Sub

        <Fact>
        Sub ToStringABiC_StandardForm_WorksAsExpected()

            Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Format ABiC0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiC0}", Cplx)}"
            Dim S2 As String = $"Format ABiC3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiC3}", Cplx)}"

            Dim Expect1 As String = "Format ABiC0: 12-15i"
            Dim Expect2 As String = "Format ABiC3: 12.100-15.400i"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)

        End Sub

        <Fact>
        Sub ToStringABiO_StandardForm_WorksAsExpected()

            Dim Cplx As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Format ABiO0: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiO0}", Cplx)}"
            Dim S2 As String = $"Format ABiO3: " +
                $"{String.Format(New ComplexStandardFormatter(), "{0:ABiO3}", Cplx)}"

            Dim Expect1 As String = "Format ABiO0: 12 - 15i"
            Dim Expect2 As String = "Format ABiO3: 12.100 - 15.400i"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)

        End Sub

    End Class ' ToString

End Namespace ' Complex
