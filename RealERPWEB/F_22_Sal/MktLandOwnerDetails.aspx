<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="MktLandOwnerDetails.aspx.cs" Inherits="RealERPWEB.F_22_Sal.MktLandOwnerDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="lblProjectList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>


                                        <asp:Label ID="lblProjectmDesc" runat="server" Visible="False" Width="350px" CssClass="lblTxt lblName txtAlgLeft">

                                        </asp:Label>
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn pull-right"></asp:Label>

                                    </div>



                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblMaterial" runat="server" CssClass="lblTxt lblName txtAlgLeft"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="10"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="chzn-select form-control" TabIndex="12">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>


                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>

                                </div>

                                  <div class="form-group">
                                    <div class=" col-md-3  pading5px asitCol3">

                                        <asp:Label ID="lblSearch" CssClass="lblTxt lblName" runat="server" Text="Unit Name"></asp:Label>
                                        <asp:TextBox ID="txtsrchunit" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="lbtnsrchunit" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lbtnsrchunit_Click" TabIndex="12"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                    </div>
                                   
                                    

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvSpayment" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowCancelingEdit="gvSpayment_RowCancelingEdit"
                            OnRowEditing="gvSpayment_RowEditing" OnRowUpdating="gvSpayment_RowUpdating">
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
                                        <asp:Label ID="lblgvItmCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usircode")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Item">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Size">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnusize" runat="server" CommandArgument="lbtnusize"
                                            OnClick="lbtnusize_Click" Style="text-align: right;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lgvamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Min Booking Money">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvminbmoney" runat="server" Style="text-align: right" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minbam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Car Parking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcparking" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cparking")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mgt Booking">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvMgtBook" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mgtbook1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:CommandField ShowEditButton="True" />

                                <asp:TemplateField HeaderText="Client Name">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">

                                            <div class="form-group">

                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:TextBox ID="txtSerachClient" runat="server" CssClass="inputTxt lblTxt inpPixedWidth" TabIndex="10"></asp:TextBox>

                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="ibtnSrchClient" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnSrchClient_Click" TabIndex="9"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                    </div>

                                                    <asp:DropDownList ID="ddlClientName" runat="server" CssClass="form-control inputTxt" Width="100" TabIndex="12">
                                                    </asp:DropDownList>

                                                </div>


                                            </div>


                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgcclientname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewPersonal" runat="server">
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="lperInfo" runat="server" CssClass="btn btn-success primaryBtn" Text="Personal Information"></asp:Label>
                                            <asp:Label ID="lblCode" runat="server" Visible="False" Width="63px"></asp:Label>
                                            <asp:LinkButton ID="lbtnBack" runat="server"
                                                OnClick="lbtnBack_Click" CssClass="btn btn-danger primaryBtn pull-right">Back</asp:LinkButton>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </fieldset>
                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="831px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" ForeColor="Black" Font-Size="11px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                    BorderColor="#660033" BorderStyle="Solid" BorderWidth="1px" Width="510px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Height="20px" Font-Size="11px"></asp:TextBox>
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
                                <div class="form-group">
                                 
                                  

                                </div>

                                

                    

                              <div class="form-group">
                                    <asp:Label ID="lperInfo0" runat="server" CssClass="btn btn-success primaryBtn" Text="Revenue Information"></asp:Label>
                                    <asp:Label ID="lblAcAmt" runat="server" Visible="False"></asp:Label>
                                    <div class="clearfix"></div>

                                </div>

                          

                                <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="831px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                    ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGcod" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnTotalCost" runat="server" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000" OnClick="lbtnTotalCost_Click">Total</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lFinalUpdateCost" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lFinalUpdateCost_Click1"> Update Cost</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgUnitnum" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "munit")) %>'
                                                    Width="50px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Size">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvUSize" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" ForeColor="Black"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "urate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvuamt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Remarks">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks"))%>'
                                                    Width="100px" Font-Size="11px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



