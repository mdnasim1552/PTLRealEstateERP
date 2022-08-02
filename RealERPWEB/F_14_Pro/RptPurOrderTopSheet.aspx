<%@ Page Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true"  CodeBehind="RptPurOrderTopSheet.aspx.cs" Inherits="RealERPWEB.F_14_Pro.RptPurOrderTopSheet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblfrmdate" runat="server">Date</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                        
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" Enabled="true" Format="dd-MMM-yyyy" TargetControlID="txtfromdate" />




                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbltodate" runat="server">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control" AutoComplete="off"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                        </div>

                        <div class="col-md-3">
                                 <asp:Label ID="Label1" runat="server">Project Name</asp:Label>                              
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control  inputTxt" AutoPostBack="True"></asp:DropDownList>
                            
                        </div>


                        <div class="col-md-1">
                            <asp:Label ID="Label14" runat="server">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                                <asp:ListItem>600</asp:ListItem>
                                <asp:ListItem>1000</asp:ListItem>
                                <asp:ListItem>2000</asp:ListItem>
                                <asp:ListItem>3000</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-6" style="margin-top:20px;" >
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary" OnClick="lnkbtnShow_Click" >Ok</asp:LinkButton>
                        </div>



                    </div>



                </div>

                <div class="card-body">
               
                     <asp:GridView ID="gvPurOrderTopSheet" runat="server" ClientIDMode="Static" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True"  AllowPaging="True"   OnPageIndexChanging="gvPurOrderTopSheet_PageIndexChanging" OnRowDataBound="gvPurOrderTopSheet_RowDataBound">
                     
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Project Name" >
                                                  <HeaderTemplate>
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Width="180px"
                                                        Text="Project Name">
                                                        <asp:HyperLink ID="hlbtntbCdataExcel" runat="server"
                                                            CssClass="btn btn-success ml-2 btn-xs" ToolTip="Export Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                    </asp:Label>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lgProName" runat="server" Width="180px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="180px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                                            </asp:TemplateField>


                                            
                                            <asp:TemplateField HeaderText="Supplier Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgSupplierName" runat="server" Width="250px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="250px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="250px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgItemName" runat="server" Width="150px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="180px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="PO No" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgPONo" runat="server" Width="120px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")).Substring(0,3) +  Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")).Substring(7,2) + " - " +  Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")).Substring(9)  %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Order Date" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrderDate" runat="server" Width="80px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="PO No(Manual)" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvOrderNoManual" runat="server" Width="120px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "oissueno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                  <FooterTemplate>                                             
                                                      
                                                      <asp:Label ID="lgpomnaual" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"> Total :</asp:Label>
                                                      </FooterTemplate>  
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>



                                              <asp:TemplateField HeaderText="Amount" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgAmount" runat="server" Width="100px" CssClass="WrpTxt"  Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                   <asp:Label ID="lgAmountFb" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>


                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>
                                          
                                             <asp:TemplateField HeaderText="Delivery Date" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgDelDate" runat="server" Width="150px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deldate")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Chalan" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lgchalan" runat="server" Width="150px" CssClass="WrpTxt"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" Width="100px" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
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