<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="AttnOutOfOffice.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_83_Att.AttnOutOfOffice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            //$('.datepicker').datepicker({
            //    format: 'mm/dd/yyyy',
            //});
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


            $('.chzn-select').chosen({ search_contains: true });



           <%-- var gvSummary = $('#<%=this.gvSaleFunnel.ClientID %>');
            gvSummary.Scrollable();--%>


        };

    </script>

    <style>

        .chzn-container-single {
            width: 200px !important;
            height: 34px !important;
        }
         .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

        /*  .project-slect  .chzn-container-single{
         width: 100px !important;
            height: 34px !important;
        
        }*/
        .profession-slect .chzn-container-single {
            height: 34px !important;
        }
    </style>

  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                    <div class="row" id="topPanle" runat="server" visible="false">

                        <div class="col-md-2 p-0">

                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary " type="button">Date</button>
                                </div>

                                <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control " AutoComplete="off"></asp:TextBox>
                                <cc1:calendarextender id="txtfromdate_CalendarExtender" runat="server"
                                    enabled="True"  targetcontrolid="txtfromdate"></cc1:calendarextender>

                            </div>
                        </div>
                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Company</button>
                                </div>
                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control  chzn-select " OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Department</button>
                                </div>
                                <asp:DropDownList ID="ddlDpt" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlDpt_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-3 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Section</button>
                                </div>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control chzn-select" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-4 p-0">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary ml-1" type="button">Emp</button>
                                </div>
                                <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control chzn-select " OnSelectedIndexChanged="ddlEmpNameAllInfo_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>
                    <div class=" clearfix"><br /></div>

                    <div class="row">
                        <div class="col-md-3">


                            <div class="form-group">
                                <label for="txtNote">Reason</label>
                             <asp:DropDownList ID="ddlReson" CssClass="form-control" runat="server">
                                 <asp:ListItem Value="F.V">Factory Visit (FV)</asp:ListItem>
                                 <asp:ListItem Value="P.V">Project Visit (PV)</asp:ListItem>
                                 <asp:ListItem Value="C.M">Client Metting (CM)</asp:ListItem>
                                 <asp:ListItem Value="O.W">Office Work (OW)</asp:ListItem>
                                 <asp:ListItem Value="O">Other (O)</asp:ListItem>
                             </asp:DropDownList>
                        </div>
                            </div>
                        <div class="col-md-12">


                            <div class="form-group">
                                <label for="txtNote">Out of Work</label>
                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSaveAttn" runat="server" OnClick="btnSaveAttn_Click" Text="Punch In" CssClass="btn btn-sm btn-success float-right" />
                            </div>

                        </div>
                    </div>



                </div>
           </div>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
