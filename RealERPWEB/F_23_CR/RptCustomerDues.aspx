<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptCustomerDues.aspx.cs" Inherits="RealERPWEB.F_23_CR.RptCustomerDues" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

   
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            var gv1 = $('#<%=this.gvcustdues.ClientID %>');
            gv1.Scrollable();


            //$('.chzn-select').chosen({
            //    search_contains: true,
            //    enable_escape_special_char: false
            //});


        }



        
    </script>






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-10 pading5px">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName"
                                            Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcProject" runat="server"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                 
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                            Width="180px"
                                            TabIndex="2">
                                        </asp:DropDownList>

                                       
                               
                                   
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to"
                                            Text="From"></asp:Label>

                                        <asp:TextBox ID="txtfrmDate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>

                                
                                          <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to"
                                            Text="To"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                         <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to " Text="Page Size"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="55px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged1">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>

                                          <asp:CheckBox ID="chkoverdues" runat="server"  Text="Over Dues" CssClass="btn btn-primary checkBox" AutoPostBack="True" OnCheckedChanged="chkoverdues_CheckedChanged" />
                                          <asp:CheckBox ID="chkCurrentdues" runat="server"  Text="Current Dues" CssClass="btn btn-primary checkBox" />
                                       
                                         </div>
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click"
                                            TabIndex="8">Ok</asp:LinkButton>
                                   

                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvcustdues" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvcustdues_PageIndexChanging"
                            ShowFooter="True"
                            OnRowDataBound="gvcustdues_RowDataBound">
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

                               <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                        


                                <asp:TemplateField HeaderText="Cutomer Name">
                                     <HeaderTemplate>
                                            <table style="width: 105px;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                            Text="Particulars" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" 
                                                              CssClass="btn btn-success btn-xs" ToolTip="Export Excel"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lgacuname" runat="server"
                                            Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Desc.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgudesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Concern Person">

                                    <ItemTemplate>
                                        <asp:Label ID="lgCper" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cteam")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Installment">

                                    <FooterTemplate>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#000"
                                            Text="Grand Total:" Font-Size="12px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvInstallment" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="110px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Schedule Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvschddate" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "schdate")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Dues Inst.">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDues" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                            Width="35px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Previous Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Current Dues">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFcurDueAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvcurDamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdueamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
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
            </div>

            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

