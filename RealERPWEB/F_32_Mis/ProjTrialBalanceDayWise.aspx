<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjTrialBalanceDayWise.aspx.cs" Inherits="RealERPWEB.F_32_Mis.ProjTrialBalanceDayWise" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {



            var gv = $('#<%=this.gvPrjtrbalance.ClientID %>');  
            gv.Scrollable();

             $('.chzn-select').chosen({ search_contains: true });
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
                                    <div class="col-md-5 pading5px ">
                                        <asp:Label ID="lblfromDate" runat="server" CssClass="lblTxt lblName" Text="From Date:"></asp:Label>
                                        
                                        <asp:TextBox ID="txtfromDate" runat="server" CssClass=" inputtextbox" TabIndex="1" AutoComplete="off"></asp:TextBox>
                                        <cc1:calendarextender id="Calfromdate" runat="server" format="dd-MMM-yyyy " targetcontrolid="txtfromDate"></cc1:calendarextender>

                                        <asp:Label ID="lbltodate" runat="server" CssClass="  smLbl_to" Text="To:" ></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox" TabIndex="1" AutoComplete="off"></asp:TextBox>
                                        <cc1:calendarextender id="txttodate_CalendarExtender" runat="server" format="dd-MMM-yyyy" targetcontrolid="txttodate"></cc1:calendarextender>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-md-3 pading5px  asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Text="Project"></asp:Label>
                                        <asp:TextBox ID="txtSearchpIndp" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindProjind" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjind_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-5 pading5px ">
                                        <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass=" chzn-select form-control inputTxt" TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblGrp" runat="server" CssClass="lblTxt lblName" Text="Group"></asp:Label>
                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                            <asp:ListItem>Main</asp:ListItem>
                                            <asp:ListItem>Sub-1</asp:ListItem>
                                            <asp:ListItem>Sub-2</asp:ListItem>
                                            <asp:ListItem>Sub-3</asp:ListItem>
                                            <asp:ListItem Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-5 pading5px">
                                        <asp:DropDownList ID="ddlProGroup" runat="server" CssClass="ddlPage" TabIndex="3"></asp:DropDownList>
                                        <asp:CheckBox ID="chkdetails" runat="server" Text="Details" CssClass="btn btn-primary checkBox" Visible="false" />
                                    </div>

                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <div class=" table table-responsive">
                          <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ProTrailBal" runat="server">
                                <asp:GridView ID="gvPrjtrbalance" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvPrjtrbalance_RowDataBound" ShowFooter="True" Width="658px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCode" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Description" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                                           %>'
                                                    Width="300px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText=" Description">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label></td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Target="_blank" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# (DataBinder.Eval(Container.DataItem, "rescode").ToString().Trim().Substring(2)=="0000000000"?
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim() :                                                             
                                           
                                                Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim()) 
                                                %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Openning (in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvOpnAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current (in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCurAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing(in Tk.)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCloseAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

