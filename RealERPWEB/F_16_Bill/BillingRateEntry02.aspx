﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BillingRateEntry02.aspx.cs" Inherits="RealERPWEB.F_16_Bill.BillingRateEntry02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });


            <%-- var gv = $('#<%=this.gvbillstatus.ClientID %>');
            gv.Scrollable();--%>

        }


    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-1 pading5px  asitCol1">
                                        <asp:Label ID="lblItem5" runat="server" CssClass="lblTxt smLbl_to lblName" Text="Project/Unit"></asp:Label>
                                        
                                    </div>

                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblProjectDesc" runat="server" Visible="False" CssClass="inputlblVal form-control " ></asp:Label>
                                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control inputTxt"></asp:DropDownList>
                                    </div>

                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="lbtnOk1" runat="server" stylr="margin-left=-50px;" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk1_Click" Text="Ok"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                           
                   
                    </fieldset>
                        <asp:Panel ID="pnlItem" Visible="false" runat="server">


                     
                    
                       <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-md-1 pading5px asitCol1 ">                                               
                                                
                                                <asp:Label ID="lblgroup" runat="server" CssClass=" lblTxt lblName" Text="Group"></asp:Label>  
                                            </div>

                                              <div class="col-md-2 pading5px  asitCol2">
                                                  <asp:DropDownList ID="ddlGroup" runat="server" CssClass="chzn-select form-control " TabIndex="12" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>


                                            <div class="col-md-1 pading5px asitCol1 ">
                                               
                                                 <asp:Label ID="lblitemList" runat="server" CssClass=" lblTxt lblName" Text="Item List"></asp:Label>
                                               
                                               
                                              
                                            </div>

                                            <div class="col-md-4 pading5px  asitCol4">
                                                  <asp:DropDownList ID="ddlitemlist" runat="server" CssClass="chzn-select form-control " TabIndex="12">
                                                </asp:DropDownList>

                                            </div>
                                            <div class="col-md-2 pading5px asitCol2">
                                                 <asp:LinkButton ID="lbtnWork" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnWork_Click">Select</asp:LinkButton>

                                                  <asp:LinkButton ID="lbtnAllWork" runat="server" CssClass="btn btn-primary primaryBtn" Style="margin-left: 20px;" OnClick="lbtnAllWork_Click">Select ALL</asp:LinkButton>
                                            </div>


                                             <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblpage" runat="server" CssClass="lblTxt lblName" Text="Page Size:" Visible="False"></asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                        Font-Bold="True" Font-Size="12px" CssClass="ddlPage"
                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="100">100</asp:ListItem>
                                        <asp:ListItem Value="150">150</asp:ListItem>
                                        <asp:ListItem Value="200">200</asp:ListItem>
                                        <asp:ListItem Value="300">300</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True" Font-Size="12px"></asp:Label>
                                </div>
                                          


                                        </div>

                                    </div>
                                </fieldset>
                              
                            
                          </asp:Panel>
               

                        <asp:GridView ID="gvRptResBasis" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            OnPageIndexChanging="gvRptResBasis_PageIndexChanging" PageSize="20"
                            ShowFooter="True" Width="654px" CssClass="table-responsive table-striped table-hover table-bordered grvContentarea" OnRowCreated="gvRptResBasis_RowCreated">
                            <PagerSettings PageButtonCount="20" Mode="NumericFirstLast" />
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="">
                                    

                                  
                                     <ItemTemplate>
                                         <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn  btn-sm" OnClientClick="return FunConfirm();" ToolTip="Delete"  OnClick="lbtnDelete_Click"><span  class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                                     </ItemTemplate>
                                    
                                    
                                  
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Group">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvgroup" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%#  "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "misirdesc"))  + "</B>"                                                                      
                                                                         
                                                                    %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="true" Font-Size="14px" />
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Work Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptRes1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc"))+ 
                                                                         (DataBinder.Eval(Container.DataItem, "sdetails").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "isirdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sdetails")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="500px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="true" Font-Size="14px" />
                                </asp:TemplateField>

                               

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRptUnit1" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn  btn-primary btn-xs" OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbgdqty" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Percent">
                                   

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpercnt" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Rate">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkfinalup" runat="server" CssClass="btn btn-danger btn-xs" OnClick="lnkfinalup_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbillrate" runat="server" BorderColor="#99CCFF" BackColor="Wheat" BorderStyle="Solid" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Amount">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFordam" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px"
                                            Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblordam" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
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
            
             </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

