
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LCOpening.aspx.cs" Inherits="RealERPWEB.F_09_LCM.LCOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="dchk1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });
        function pageLoaded() {

            var gridview = $('#<%=this.gvPersonalInfo.ClientID %>');
            gridview.ScrollableGv();

       <%--     var grdVCr = $('#<%=this.dgvOrder.ClientID %>');
            grdVCr.ScrollableGv();--%>

            var gridview3 = $('#<%=this.dgvOrder.ClientID %>');
            $.keynavigation(gridview3);

         

           
            $('#tblrpprodetails').Scrollable({
            });
            

           
        };

    </script>

<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

            <div class="container moduleItemWrpper">
                <div class="contentPart">





                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">




                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-1 pading5px">

                                        <asp:Label ID="lblLcno" runat="server" CssClass="lblTxt lblName">L/C Number</asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlLcCode" runat="server" CssClass=" chzn-select form-control"  AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkOpen" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOpen_Click">Open</asp:LinkButton>
                                           
                                        </div>
                                          <div class="colMdbtn pull-right">
                                         <asp:HyperLink ID="hlbtnnew" NavigateUrl="~/F_17_Acc/AccCodeBook.aspx?InputType=Accounts" Target="_blank" CssClass="btn btn-primary btn-xs" runat="server">NEW LC</asp:HyperLink>
                                              </div>
                                    </div>
                                      <div class="col-md-1">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnReq" Visible="false" OnClientClick="return confirm('Really Do you want to Build a link with this LC and Requistion?')" runat="server" CssClass="btn btn-success btn-xs" OnClick="lbtnReq_Click">Requisition Link</asp:LinkButton>
                                        </div>
                                    </div>
                                     <div class="col-md-2">
                                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                            </div>
                                    <div class="col-md-3 pull-right">
                                         <div class="msgHandSt">


                                        <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Labelpro" runat="server" CssClass="lblProgressBar"
                                                    Text="Please Wait.........."></asp:Label>

                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>

                                    </div>
                                  
                                    </div>
                                </div>
                                <asp:TextBox ID="txtconv" runat="server" Visible="false"></asp:TextBox>
                                <div class="col-md-4">
                                     <asp:Panel ID="Panel3" runat="server" Visible="false">
                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="380px" CssClass="table-striped table-hover table-bordered grvContentarea mygvinputbox">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"  Style="font-size:10px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="120px"></asp:Label>

                                            </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                    Width="2px"></asp:Label>
                                            </ItemTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnSaveCust" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkbtnSaveCust_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Height="20px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="200px"></asp:TextBox>
                                                <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                <asp:Panel ID="PanelOther" runat="server">

                                                    <div class="form-group" style="margin-bottom:0px;">
                                                        <div class="col-md-12 pading5px">
                                                            <asp:DropDownList ID="ddlAlType" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="200" AutoPostBack="true" TabIndex="2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                </asp:Panel>

                                                <asp:Panel ID="pnlcurrency" runat="server">

                                                    <div class="form-group" style="margin-bottom:0px;">
                                                        <div class="col-md-12 pading5px">
                                                            <asp:DropDownList ID="ddlcurrency" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="200" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlcurrency_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                </asp:Panel>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                    
                                </asp:Panel>
                                </div>
                                <div class="col=-md-8">

                                      <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewOrder" runat="server">
                                <fieldset class="scheduler-border fieldset_B">



                                    <div class="form-group">
                                         <asp:Label ID="lblproduct" runat="server" CssClass=" col-md-1  smLbl_to">Resource:</asp:Label>
                                        <div class="col-md-4">
                                              <asp:DropDownList ID="ddlResList" runat="server" CssClass="inputTxt chzn-select" AutoPostBack="True" Width="225px" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>

                                        </div>
                                          <asp:Label ID="Label1" runat="server" CssClass=" col-md-2  smLbl_to">Specification:</asp:Label>
                                      
                                        <div class="col-md-3">                                            
                                       <asp:DropDownList ID="ddlResSpcf" runat="server" Width="220px" CssClass="smDropDown inputTxt chzn-select"  TabIndex="3"></asp:DropDownList>

                                        </div>
                                        
                                    <div class="col-md-1 ">
                                        <asp:LinkButton ID="lnkAddTable" Style="margin-left:15px;" runat="server" CssClass="btn btn-primary  btn-xs " OnClick="lnkAddTable_Click">Add <span class="glyphicon glyphicon-plus"></span></asp:LinkButton>
                                                                           </div>
                                    </div>
                               
                                </fieldset>


                                <div class="table-responsive">
                                    <asp:GridView ID="dgvOrder" runat="server"  OnRowDeleting="dgvOrder_RowDeleting"
                                        AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                        Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code" 
                                                ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCode" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkpsame" Style="font-size:10px;" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lnkSameValue_Click">Put Same</asp:LinkButton>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvscode" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "scode")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="9px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description" ItemStyle-HorizontalAlign="Left">
                                                  <HeaderTemplate>
                                        <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Resource Description" Width="120"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-success" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lnkTotal_Click">Total Calc</asp:LinkButton>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvResdesc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left"  Width="200"/>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvspc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="spcfcode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvspccode" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                             </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvUnit" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFOrderQty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066" Width="50px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgrvOrderQty" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="10px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <FooterStyle Font-Bold="true" Font-Size="10px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Free Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgrvFreeqty" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="10px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFFreeqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FC Rate">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkFinalUpdate" runat="server" OnClick="lnkFinalUpdate_Click" CssClass="btn btn-danger primaryBtn">Update</asp:LinkButton>
                                                                                                    </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgrvRate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" FC Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFamount" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvamount" runat="server" Font-Size="10px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle ForeColor="#CC0066" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BDT Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFBDTamount" runat="server" Font-Bold="True" Font-Size="10px"
                                                        ForeColor="#CC0066" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvBDTamount" runat="server" Font-Size="10px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdamount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle ForeColor="#CC0066" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:CommandField ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='glyphicon glyphicon-remove'></span>" HeaderText="" ItemStyle-Font-Size="10px"
                                                ShowDeleteButton="True">
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" />
                                                <ItemStyle Font-Size="10px" />
                                            </asp:CommandField>
                                        </Columns>

                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                                          
                        </asp:MultiView>
                        <div>
                            <asp:Label ID="lblPrintMsg" runat="server" CssClass="FormLevel"></asp:Label>
                           
                        </div>

                                </div>
                            </div>
                        </fieldset>



                    </div>

                </div>
                </fieldset>
                
                    <div class="row">



                      

                    </div>
            </div>
          

      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
