<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktLOnerUnitEntry.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktLOnerUnitEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
       
        function pageLoaded() {
            var gv = $('#<%=this.gvUnit.ClientID %>');
           gv.Scrollable();


           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);

           });
           var gridview = $('#<%=this.gvUnit.ClientID %>');
            $.keynavigation(gridview);

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

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
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            
                                             
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control" TabIndex="3" >
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
                               
                            </div>

                        </fieldset>
                    </div>
                     <asp:Panel ID="PanelGroup" runat="server"  Visible="False">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                              <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                
                               <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblMaterials" runat="server" CssClass="lblTxt lblName"  Text="Group Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcMat" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnGroup" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnGroup_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" TabIndex="4">
                                        </asp:DropDownList>


                                    </div>


                                </div>

                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName"  Text="Floor Name"></asp:Label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFloor" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFloor_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlFloor" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged" TabIndex="6">
                                        </asp:DropDownList>


                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page Size" ></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                                    BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" 
                                                    Width="70px">
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                    <asp:ListItem Value="400">400</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:CheckBox ID="chkAllSInf" runat="server" AutoPostBack="True"
                                                    Font-Bold="True"
                                                    OnCheckedChanged="chkAllSInf_CheckedChanged" Text="Show All" CssClass="btn btn-primary checkBox" TabIndex="7" />
                                        
                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="Total No"></asp:Label>

                                        <asp:TextBox ID="txttotalno" runat="server" CssClass=" smLbl_to" Width="100px"></asp:TextBox>

                                         <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnTotal" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnTotal_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                       

                                    </div>

                                </div>
                            </div>

                        </fieldset>
                        </fieldset>
                    </div>
                         </asp:Panel>
                     
                                          
                      <div class="table-responsive">
                                <asp:GridView ID="gvUnit" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvUnit_PageIndexChanging"
                                    OnRowDataBound="gvUnit_RowDataBound" OnRowDeleting="gvUnit_RowDeleting"
                                    ShowFooter="True">
                                    <RowStyle  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Code">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn btn-primary checkBox">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCod" runat="server" Font-Size="11px" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                       

                                        <asp:TemplateField HeaderText="Description of Item">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lFinalUpdate" runat="server" onclick="lFinalUpdate_Click"  CssClass="btn  btn-danger primarygrdBtn">Final Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemdesc" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="120px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgUnitnum" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Size">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUSize" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lFUsize" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Apt. Qty" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUqty" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvfAptqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

