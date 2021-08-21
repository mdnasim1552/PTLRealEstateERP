<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="EntryLostSoldAndDes01.aspx.cs" Inherits="RealERPWEB.F_13_Cen.EntryLostSoldAndDes01" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      
       <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

        };

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
                                <asp:Panel ID="pnlMain" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-5  pading5px  asitCol10">

                                            <asp:Label ID="Label15" runat="server" CssClass=" lblName lblTxt" Text="Project:"></asp:Label>

                                            <asp:TextBox ID="txtsrchProject" runat="server" CssClass="inputtextbox "></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlProject" runat="server" Width="280px" CssClass="chzn-select ddlPage"></asp:DropDownList>

                                            <%-- <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>--%>

                                            <asp:Label ID="lblddlProject" runat="server" __designer:wfdid="w4" CssClass="inputtextbox" Visible="False" Width="270px"></asp:Label>
                                             
                                        </div>
                                        <div>
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="Label8" runat="server" CssClass=" lblName lblTxt" Text="Date.:"></asp:Label>

                                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label16" runat="server" CssClass=" smLbl_to" Text="No.:"></asp:Label>

                                            <asp:Label ID="lblCurNo1" runat="server" CssClass="inputtextbox"></asp:Label>

                                            <asp:TextBox ID="txtCurNo2" runat="server" CssClass="inputtextbox">00001</asp:TextBox>

                                            <asp:Label ID="Label14" runat="server" CssClass="smLbl_to" Text="Ref. No:"></asp:Label>

                                            <asp:TextBox ID="txtrefno" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                           

                                            <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblPreViousList" runat="server" CssClass=" lblName lblTxt" Text="Previous:"></asp:Label>

                                            <asp:TextBox ID="txtSrchPrevious" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindPrevious" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindPrevious_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlPreList" runat="server" AutoPostBack="True" Width="280px" CssClass="ddlPage"></asp:DropDownList>

                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelSub" runat="server" Visible="False">
                                    <div class="form-group">
                                        <div class="col-md-6  pading5px  asitCol10">

                                            <asp:Label ID="lblResList" runat="server" CssClass=" lblName lblTxt" Text="Materials:"></asp:Label>

                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" Width="322px" CssClass="chzn-select ddlPage"></asp:DropDownList>

                                        </div>
                                        <div class="col-md-5">
                                            <asp:Label ID="lblSpecification" runat="server" CssClass=" lblName lblTxt" Text="Specification:"></asp:Label>

                                            <asp:TextBox ID="txtSrchSpecification" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnSpecification" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnSpecification_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                     <div class="table table-responsive">
                         <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                ShowFooter="True" Width="501px">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblgvMatCode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Resource Description">
                        <ItemTemplate>
                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                Width="180px"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Specification">
                        <ItemTemplate>
                            <asp:Label ID="lbgvspcfdesc" runat="server" Style="text-align: left"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                Width="120px"></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit">
                        <ItemTemplate>
                            <asp:Label ID="lblgvunit" runat="server"
                                Style="font-size: 11px; text-align: center;"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                Width="50px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnktotal" runat="server" Font-Bold="True" Font-Size="12px"
                                ForeColor="#000" OnClick="lnktotal_Click">Total</asp:LinkButton>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Stock Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblgvstkqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>


                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Stock  Rate">
                        <ItemTemplate>
                           <%-- <asp:Label ID="lblgvstkrate" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:Label>--%>


                            <asp:TextBox ID="lblgvstkrate" runat="server" BackColor="Transparent" BorderStyle="None"  Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px" ></asp:TextBox>

                        </ItemTemplate>


                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Lost">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvlqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="lnkupdate" runat="server"  CssClass="btn btn-danger primaryBtn" OnClick="lnkupdate_Click">Update</asp:LinkButton>
                        </FooterTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sold">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvsqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>


                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Destroyed">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvdqty" runat="server" BackColor="Transparent" BorderStyle="None"
                                Style="text-align: right; font-size: 11px;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="70px"></asp:TextBox>
                        </ItemTemplate>


                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Amount">
                        <FooterTemplate>
                            <asp:Label ID="lgvFAmount" runat="server" Style="text-align: right"
                                Width="100px"></asp:Label>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblamt" runat="server"
                                Style="font-size: 11px; text-align: right;"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lsdam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                Width="100px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000"
                            HorizontalAlign="right" VerticalAlign="Middle" />

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

