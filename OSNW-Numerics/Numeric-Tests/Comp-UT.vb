Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports Xunit
Imports OSNW.Numerics

Namespace Complex

    Public Class AdHoc

        <Fact>
        Sub DocsExample_Works_AsExpected()

            Dim c1 As New System.Numerics.Complex(12.1, -15.4)

            Dim S1 As String = $"Formatting with ToString():       {c1}"
            Dim S2 As String = $"Formatting with ToString(format): {c1:N2}"
            Dim S3 As String = $"Custom formatting with I0:        " +
                $"{String.Format(New ComplexFormatter(), "{0:I0}", c1)}"
            Dim S4 As String = $"Custom formatting with J3:        " +
                $"{String.Format(New ComplexFormatter(), "{0:J3}", c1)}"

            Dim Expect1 As String = "Formatting with ToString():       <12.1; -15.4>"
            Dim Expect2 As String = "Formatting with ToString(format): <12.10; -15.40>"
            Dim Expect3 As String = "Custom formatting with I0:        12 - 15i"
            Dim Expect4 As String = "Custom formatting with J3:        12.100 - 15.400j"

            Assert.Equal(Expect1, S1)
            Assert.Equal(Expect2, S2)
            Assert.Equal(Expect3, S3)
            Assert.Equal(Expect4, S4)

        End Sub



        <Fact>
        Sub ABI_Works_AsExpected()

            Dim c1 As New System.Numerics.Complex(12.1, -15.4)

            Dim S3 As String = $"Custom formatting with ABI0:        " +
                $"{String.Format(New ComplexFormatterABI(), "{0:ABI0}", c1)}"
            Dim S4 As String = $"Custom formatting with ABI3:        " +
                $"{String.Format(New ComplexFormatterABI(), "{0:ABI3}", c1)}"

            Dim Expect3 As String = "Custom formatting with ABI0:        12 - 15i"
            Dim Expect4 As String = "Custom formatting with ABI3:        12.100 - 15.400i"

            Assert.Equal(Expect3, S3)
            Assert.Equal(Expect4, S4)

        End Sub



        <Fact>
        Sub AIB_Works_AsExpected()

            Dim c1 As New System.Numerics.Complex(12.1, -15.4)

            Dim S3 As String = $"Custom formatting with AIB0:        " +
                $"{String.Format(New ComplexFormatterAIB(), "{0:AIB0}", c1)}"
            Dim S4 As String = $"Custom formatting with AIB3:        " +
                $"{String.Format(New ComplexFormatterAIB(), "{0:AIB3}", c1)}"

            Dim Expect3 As String = "Custom formatting with AIB0:        12 - i15"
            Dim Expect4 As String = "Custom formatting with AIB3:        12.100 - i15.400"

            Assert.Equal(Expect3, S3)
            Assert.Equal(Expect4, S4)

        End Sub

    End Class









    Public Class General

        <Fact>
        Sub Complex_Constructor_Works_AsExpected()
            Dim Z As New System.Numerics.Complex(125.125, 50.5)
            Assert.Equal(125.125, Z.Real)
            Assert.Equal(50.5, Z.Imaginary)
        End Sub

    End Class ' General

    Public Class ToString

        <Theory>
        <InlineData(125.125, 50.5, "<125.125; 50.5>")>
        <InlineData(-125.125, 50.5, "<-125.125; 50.5>")>
        <InlineData(125.125, -50.5, "<125.125; -50.5>")>
        <InlineData(-125.125, -50.5, "<-125.125; -50.5>")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "<3.4028234663852886E+38; -3.4028234663852886E+38>")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)
            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToString
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToString

    Public Class ToStringAIB

        <Theory>
        <InlineData(125.125, 50.5, "125.125+i50.5")>
        <InlineData(-125.125, 50.5, "-125.125+i50.5")>
        <InlineData(125.125, -50.5, "125.125-i50.5")>
        <InlineData(-125.125, -50.5, "-125.125-i50.5")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "3.4028234663852886E+38-i3.4028234663852886E+38")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)
            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToStringAIB
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToStringAIB

    Public Class ToStringABI

        <Theory>
        <InlineData(125.125, 50.5, "125.125+50.5i")>
        <InlineData(-125.125, 50.5, "-125.125+50.5i")>
        <InlineData(125.125, -50.5, "125.125-50.5i")>
        <InlineData(-125.125, -50.5, "-125.125-50.5i")>
        <InlineData(Single.MaxValue, Single.MinValue,
                    "3.4028234663852886E+38-3.4028234663852886E+38i")> ' Encourage exponential notation
        Sub Works_AsExpected(ByVal real As Double, ByVal imaginary As Double, ByVal expected As String)
            Dim Z As New System.Numerics.Complex(real, imaginary)
            Dim Result As System.String = Z.ToStringABI()
            Assert.Equal(expected, Result)
        End Sub

    End Class ' ToStringABI

End Namespace ' Complex
