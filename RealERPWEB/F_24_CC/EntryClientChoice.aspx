<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryClientChoice.aspx.cs" Inherits="RealERPWEB.F_24_CC.EntryClientChoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            
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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName"  Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1" ></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            
                                             
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" Visible="false" CssClass="form-control inputTxt"></asp:Label>

                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pull-right pading5px">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>
                                    </div>

                                </div>
                               
                         
                           <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblscustomer" runat="server" CssClass="lblTxt lblName">Customer Name</asp:Label>
                                        <asp:TextBox ID="txtSrcCustomer" runat="server" CssClass=" inputtextbox" ></asp:TextBox>


                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindCustomer" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindCustomer_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="chzn-select form-control inputTxt"  Width="290px">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCustomer" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                    </div>
                                </div>

                              

                        </fieldset>
                        <asp:GridView ID="gvclientchoice" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="431px" CssClass="table table-striped table-hover table-bordered grvContentarea"  OnSelectedIndexChanged="gvclientchoice_SelectedIndexChanged">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Head">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            OnClick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvhead" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: Left; background-color: Transparent"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgdesc")).Trim().Length>0 ?  "<br>" : "")+  
    
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim(): "") %>'
                                                                        Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Client Choice">
                                      
                                   
                                    <ItemTemplate>
                                        <asp:textBox ID="txtgvclientc" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wdesc")) %>'
                                            Width="200px" ></asp:textBox>
                                    </ItemTemplate>
                                      
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Unit">
                                     <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                            Font-Size="12px" OnClick="lbtnUpdate_Click" CssClass="btn  btn-danger primarygrdBtn"> Update </asp:LinkButton>
                                    </FooterTemplate>
                                    
                                     
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunit" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Unit")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="QTY">
                                      
                                   
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvqty" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="12px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;#,##0; ") %>'
                                            Width="60px" ></asp:TextBox>
                                    </ItemTemplate>
                                      
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                
                                


                                 <asp:TemplateField HeaderText="Company Standard rate">
                                   
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcrate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crate")).ToString("#,##0;#,##0; ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Client Rate">
                                     
                                    
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvclrate" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align:right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clrate")).ToString("#,##0; #,##0; ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                        VerticalAlign="Middle" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->









        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

