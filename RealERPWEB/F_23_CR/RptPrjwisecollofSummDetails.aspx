<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptPrjwisecollofSummDetails.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptPrjwisecollofSummDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .btn-space {
            margin-right: 120px;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
           <%-- var gv = $('#<%=this.gvSubBill.ClientID %>');
            gv.Scrollable();--%>
    }


    </script>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 asitCol3 pading5px">

                                <asp:Label ID="Label5" runat="server"
                                    CssClass="lblTxt lblName" Text=" Name:"></asp:Label>

                                <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"
                                    Font-Bold="True"></asp:TextBox>
                                <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>
                            <div class="col-md-4 asitCol4 pading5px">
                                <asp:DropDownList ID="ddlProjectName" CssClass=" ddlPage chzn-select" runat="server" Font-Bold="True"
                                    Width="300px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectName_ListSearchExtender2" runat="server"
                                    QueryPattern="Contains" TargetControlID="ddlProjectName">
                                </cc1:ListSearchExtender>


                            </div>
                            <div class="col-md-1 pading5px pull-left">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>



                        </div>
                        <div class="form-group">
                            <div class="col-md-12 ">

                                <%--<asp:Label ID="lblPage0" runat="server" CssClass="lblTxt lblName" Text="Size:" ></asp:Label>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    CssClass="ddlPage"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Width="71px">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>--%>

                                <asp:Label ID="Label7" runat="server" CssClass=" lblTxt lblName" Text="Date:"></asp:Label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputTxt inpPixedWidth" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>

                                <asp:Label ID="Label8" runat="server" CssClass="smLbl_to" Text="To:"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                    CssClass="inputTxt inpPixedWidth"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                <%-- <asp:Label ID="Label7" runat="server" CssClass="smLbl_to" Text="Date:"> </asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>--%>
                            </div>
                        </div>

                    </div>

                </fieldset>
                <div class="table table-responsive">
                    <asp:GridView ID="gvprjstatus" runat="server" AllowPaging="false"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvprjstatus_PageIndexChanging" OnRowDataBound="gvprjstatus_RowDataBound"
                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvprjmcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjmcode")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText=" Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lgvprjcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prjcode")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lgvName" runat="server"
                                        Text='<%#  "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc"))+"</b>" %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name">

                                <HeaderTemplate>
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Project Name" Width="200px"></asp:Label>

                                    <asp:HyperLink ID="hlbtntbCdataExcel" runat="server" CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvpactdesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>

                                <FooterTemplate>
                                </FooterTemplate>


                                <HeaderStyle HorizontalAlign="Left" />


                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Collection Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvmramt" runat="server" Style="text-align: right" BorderStyle="None"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mramt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>


                                <FooterTemplate>
                                    <asp:Label ID="lgvFmramt" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>

                                </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>





                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>

                <asp:Panel ID="pnlavg" runat="server" Visible="false">
                    <fieldset class="fieldset_Nar">
                        <div class="form-horizontal">


                            <div class="form-group">
                                <div class="col-md-3 pading5px ">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lbltotal" runat="server" CssClass="lblTxt lblName" Text="Total Collection :"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txttotal" runat="server" Style="margin-left: 7px; text-align: right;" Width="90px" class="ddlPage62 inputTxt"></asp:TextBox>
                                    </div>


                                </div>

                                <div class="col-md-3 pading5px">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lblavgamt" runat="server" CssClass="lblTxt lblName" Text="Average Collection :"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txtavgAmt" runat="server" Style="margin-left: 7px; text-align: right;" Width="90px" class="ddlPage62 inputTxt"></asp:TextBox>
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lblmon" runat="server" CssClass="lblTxt lblName" Text="Total Month :"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txtmonth" runat="server" Style="margin-left: 7px; text-align: left;" Width="50px" class="ddlPage62 inputTxt"></asp:TextBox>



                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px ">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lblnetcoll" runat="server" CssClass="lblTxt lblName" Text=" Net Collection :"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txtNetcoll" runat="server" Style="margin-left: 7px; text-align: right;" Width="90px" class="ddlPage62 inputTxt"></asp:TextBox>
                                    </div>


                                </div>

                                <div class="col-md-3 pading5px">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon">
                                            <asp:Label ID="lblavgcoll" runat="server" CssClass="lblTxt lblName" Text="Avg Net Collection :"></asp:Label>
                                        </span>
                                        <asp:TextBox ID="txtnetavgcoll" runat="server" Style="margin-left: 7px; text-align: right;" Width="90px" class="ddlPage62 inputTxt"></asp:TextBox>




                                    </div>
                                </div>
                            </div>
                        </div>






                    </fieldset>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>

