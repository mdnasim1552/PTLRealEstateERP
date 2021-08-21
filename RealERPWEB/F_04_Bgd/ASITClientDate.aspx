
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ASITClientDate.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.ASITClientDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
      $(document).ready(function () {

          FunCurDate();
        
      });



    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                       
                        <asp:LinkButton ID="lbtnShow" runat="server" OnClick="lbtnShow_Click">Show</asp:LinkButton>


                    </div>







                </div>
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



