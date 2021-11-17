<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustMaintenanceWork.aspx.cs" Inherits="RealERPWEB.F_24_CC.CustMaintenanceWork" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
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
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblType" runat="server" CssClass="lblTxt lblName" Text="Type"></asp:Label>
                                        <asp:TextBox ID="txtSearchType" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnType" CssClass="btn btn-primary srearchBtn" runat="server" onclick="ibtnType_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-2">

                                        <asp:DropDownList ID="ddlType" runat="server" Width="200px" CssClass="chzn-select inputTxt" TabIndex="3"></asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 pading5px asitCol3">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>


                                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>

                                    </div>
                           
                                    <div class="col-md-2 pading5px asitCol3" >
                                        <asp:Label ID="lblPrevious" runat="server" CssClass="lblTxt lblName" Text="Previous"></asp:Label>

                                        <asp:TextBox ID="txtPreAdNo" runat="server" CssClass=" inputtextbox" TabIndex="3"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnPreAdNo" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnPreAdNo_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                         


                                    </div>
                                    <div class="col-md-3 pading5px " >
                                        <asp:DropDownList ID="ddlPrevADNumber" runat="server" CssClass="inputTxt" TabIndex="3" Width="280px"></asp:DropDownList>
                                    </div>
                                       
<%--                                    <div class="col-md-4 pading5px asitCol4">--%>

                                       

                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblSchCode" runat="server" Visible="false"></asp:Label>
                                    </div>

                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                    <%--<div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblType" runat="server" CssClass="lblTxt lblName" Text="Type"></asp:Label>


                                        <asp:TextBox ID="txtSearchType" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                        <asp:LinkButton ID="ibtnType" CssClass="btn btn-primary srearchBtn" runat="server" onclick="ibtnType_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">

                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control inputTxt" TabIndex="3"></asp:DropDownList>

                                    </div>--%>

                                    <div class="clearfix"></div>

                                </div>
                                <div class="form-group">
                                    <%--<div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Date"></asp:Label>


                                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Add.No"></asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>

                                    </div>--%>

                                    <div class="clearfix"></div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPreList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">

                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="true" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="False"></asp:Label>
                                         
                                    </div>
                                   <div>
                                       <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn"
                                            TabIndex="7">Ok</asp:LinkButton>
                                   </div>
                                    <div class="col-md-2  asitCol4">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Add.No"></asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" CssClass="inputTxt inputDateBox"></asp:Label>

                                    </div>
                                    <div class="clearfix"></div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Unit Name"></asp:Label>


                                        <asp:TextBox ID="txtsrchUnitName" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindUnitName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindUnitName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">

                                        <asp:DropDownList ID="ddlUnitName" runat="server" CssClass="form-control inputTxt chzn-select" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged "></asp:DropDownList>
                                        
                                        <asp:Label ID="lblUnitName" runat="server" CssClass="form-control inputTxt" Visible="False"></asp:Label>

                                    </div>

                                    <div class="clearfix"></div>

                                </div>

                            </div>
                        </fieldset>

                    </div>
                   
                        <div class="row">

                            <asp:Panel ID="PanelItem" runat="server" Visible="False">
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName" Text="Resource List"></asp:Label>
                                                  <asp:TextBox ID="txtsrchItem" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindAdWork" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindAdWork_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">

                                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="3"></asp:DropDownList>

                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lbtnAddWork" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnAddWork_Click" TabIndex="2">Add </span></asp:LinkButton>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" ></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>





                            </asp:Panel>
                        </div>


             
                    <div class="row">
                        <div class="table table-responsive">
                        <asp:GridView ID="gvAddWork" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea "
                            ShowFooter="True" Width="543px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />                             
                                </asp:TemplateField>

                                
                                 <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdWrkdelete" runat="server" OnClick="lbtnAdWrkdelete_Click" ><span class="glyphicon glyphicon-remove"> </span></asp:LinkButton>
                                    </ItemTemplate>

                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGcodAdd" runat="server" ForeColor="Black" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotalAddWork" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalAddWork_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Location">
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvlocateion" runat="server" ForeColor="Black" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location")) %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Description of Item">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdateAdWork" runat="server"  CssClass="btn btn-danger primaryBtn" OnClick="lFinalUpdateAdWork_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgdescw" runat="server" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Client Choice">
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvdesclchoice" runat="server" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkdesc")) %>'
                                            Width="150px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Unit">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lgvunit" runat="server" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Company Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcomqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Company Standard  M.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcommRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Company Standard L.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcomlRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comlrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Company Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcomAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "comamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Client Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Client Choice  M.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvclmRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Client Choice  L.Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcllRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cllrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>



                                 <asp:TemplateField HeaderText="Client Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Difference">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldiffgvRate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Net Refund">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnrefund" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nrefund")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnrefund" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Net Demand">
                                    <ItemTemplate>
                                        <asp:Label ID="lblndemand" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ndemand")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFndemand" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                         <asp:TextBox ID="txtgvdiscount" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "disamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFdisAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                      <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Net Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvnetamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" ForeColor="Black" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFnetAmt" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
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


                      <asp:Panel ID="PnlNarration" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarr" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                            </div>
                                        </div>

                                      

                                        <div class="clearfix"></div>
                                    </div>

                              

                                </div>

                            </fieldset>
                          </asp:Panel>



                </div>
            </div>






            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


