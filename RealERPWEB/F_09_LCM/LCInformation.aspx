
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LCInformation.aspx.cs" Inherits="RealERPWEB.F_09_LCM.LCInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="dchk1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

           <%-- var gridview = $('#<%=this.gvPersonalInfo.ClientID %>');
            gridview.ScrollableGv();

            var grdVCr = $('#<%=this.dgvOrder.ClientID %>');
            grdVCr.ScrollableGv();





            var gridview3 = $('#<%=this.dgvOrder.ClientID %>');
            $.keynavigation(gridview3);

            var gridview1 = $('#<%=this.dgvReceive.ClientID %>');
            $.keynavigation(gridview1);

            $('.chzn-select').chosen({ search_contains: true });
            $('#tblrpprodetails').Scrollable({
            });
            --%>

           
        };

    </script>
    <style>
    .btnlgn {
    margin-bottom: 2px;
    }
    fieldset > legend:first-of-type
    {
    -webkit-margin-top-collapse: separate;
    margin-bottom: 20px;
    }
    </style>



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
                                    <div class="col-md-1 pading5px">

                                        <asp:Label ID="lblLcno" runat="server" CssClass="lblTxt lblName">L/C Number</asp:Label>
                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlLcCode" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lnkOpen" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOpen_Click">Open</asp:LinkButton>
                                        </div>
                                    </div>
                                     
                                    <div class="col-md-3 pull-right">
                                         <div class="msgHandSt">


                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Labelpro" runat="server" CssClass="lblProgressBar"
                                                    Text="Please Wait.........."></asp:Label>

                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                    </div>
                                       <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageAlign="Right" ImageUrl="~/Image/waitl.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>
                                    </div>
                                </div>
                                <asp:TextBox ID="txtconv" runat="server" Visible="false"></asp:TextBox>
                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                    <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" Width="760px" CssClass="table-striped table-hover table-bordered grvContentarea mygvinputbox">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
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
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>

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
                                                    Width="410px"></asp:TextBox>
                                                <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent" BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="200px"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>
                                                <asp:Panel ID="PanelOther" runat="server">

                                                    <div class="form-group" style="margin-bottom:0px;">
                                                        <div class="col-md-12 pading5px">
                                                            <asp:DropDownList ID="ddlAlType" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="150" AutoPostBack="true" TabIndex="2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>


                                                </asp:Panel>

                                                <asp:Panel ID="pnlcurrency" runat="server">

                                                    <div class="form-group" style="margin-bottom:0px;">
                                                        <div class="col-md-12 pading5px">
                                                            <asp:DropDownList ID="ddlcurrency" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="150" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlcurrency_SelectedIndexChanged">
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
                        </fieldset>



                    </div>

                </div>
                </fieldset>
                
                    <div class="row">



                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewOrder" runat="server">
                                <fieldset class="scheduler-border fieldset_B">


                                    <legend class="btnlgn"><span class="btn btn-success primaryBtn btn-sm">Purchase Order</span></legend>
                                    <div class="form-group">
                                         <asp:Label ID="lblproduct" runat="server" CssClass=" col-md-2 lblTxt lblName">Resource:</asp:Label>
                                        <div class="col-md-3">
                                              <asp:DropDownList ID="ddlResList" runat="server" CssClass="inputTxt chzn-select" AutoPostBack="True" Width="320px" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>

                                        </div>
                                          <asp:Label ID="Label1" runat="server" CssClass=" col-md-2  pading5px lblTxt lblName" Style="margin-left:20px;">Specification:</asp:Label>
                                      
                                        <div class="col-md-3">                                            
                                       <asp:DropDownList ID="ddlResSpcf" runat="server" Width="250px" CssClass="smDropDown inputTxt chzn-select"  TabIndex="3"></asp:DropDownList>

                                        </div>
                                    </div>
                                   





                                   <%-- <dchk1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" MaxDropDownHeight="200"
                                        TabIndex="8" TransitionalMode="True" Width="320px">
                                    </dchk1:DropCheck>--%>

                                     


                                    <div class="col-md-2 " style="margin-top: -20px;">
                                        <asp:LinkButton ID="lnkAddTable" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkAddTable_Click">Add Table</asp:LinkButton>
                                        <%--<asp:LinkButton ID="lnkAddAll" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkAddAll_Click">Select All</asp:LinkButton>--%>
                                    </div>










                                </fieldset>


                                <div class="table-responsive">
                                    <asp:GridView ID="dgvOrder" runat="server"  OnRowDeleting="dgvOrder_RowDeleting"
                                        AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                        Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCode" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkpsame" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lnkSameValue_Click">Put Same Value</asp:LinkButton>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvscode" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "scode")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description" ItemStyle-HorizontalAlign="Left">
                                                  <HeaderTemplate>
                                        <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Resource Description" Width="200"></asp:Label>
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
                                                    <asp:Label ID="lblgrvResdesc" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvspc" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="spcfcode" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvspccode" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                             </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvUnit" runat="server" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFOrderQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgrvOrderQty" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Free Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgrvFreeqty" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFFreeqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FC Rate">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkFinalUpdate" runat="server" OnClick="lnkFinalUpdate_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgrvRate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="90px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" FC Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFamount" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvamount" runat="server" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle ForeColor="#CC0066" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BDT Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgrvFBDTamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#CC0066" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvBDTamount" runat="server" Font-Size="12px" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdamount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle ForeColor="#CC0066" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:CommandField DeleteText="Delete" HeaderText="Delete" ItemStyle-Font-Size="12px"
                                                ItemStyle-ForeColor="Red" ShowDeleteButton="True">
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                <ItemStyle Font-Size="12px" ForeColor="Red" />
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
                            <asp:View ID="ViewReceive" runat="server">
                                <fieldset class="scheduler-border fieldset_B">
                                    <div class="form-horizontal">





                                    </div>
                                    <legend class="btnlgn"><span class="btn btn-success primaryBtn btn-sm">Purchase Receive</span></legend>
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px">
                                            <asp:Label ID="lblreceivedat" runat="server" CssClass="lblTxt lblName">Receive Date:</asp:Label>
                                            <asp:TextBox ID="txtreceivedate" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalExr2" runat="server" Format="dd-MMM-yyyy ddd" TargetControlID="txtreceivedate" />
                                            <asp:Label ID="lblgrr" runat="server" CssClass="lblTxt lblName">GRN No.</asp:Label>
                                            <asp:TextBox ID="txtgrrno" runat="server" CssClass=" inputtextbox" ReadOnly="True" TabIndex="14" Width="100px"></asp:TextBox>
                                            <asp:CheckBox ID="chkExel" runat="server" AutoPostBack="True" CssClass="btn btn-warning primaryBtn checkBox" OnCheckedChanged="chkExel_CheckedChanged" TabIndex="9" Text="Input From Exel?" />
                                            <asp:Panel ID="pnlExel" runat="server" style="margin-top:-5px;" TabIndex="22" Visible="False">
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Exel File"></asp:Label>
                                                        <div class="uploadFile">
                                                            <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblstorid" runat="server" CssClass="lblTxt lblName">Store Id:</asp:Label>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlStorid" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select" TabIndex="16">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkOk_Click" Visible="False">OK</asp:LinkButton>
                                            <asp:LinkButton ID="lnkReceive" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkReceive_Click">Receive</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblPreGrn" runat="server" CssClass="lblTxt lblName">Pre GRN:</asp:Label>
                                            <asp:TextBox ID="txtsrGrn" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnPreGrn" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnPreGrn_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlPreGrn" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select" TabIndex="16">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                
                                </fieldset>


                               
                                    <asp:GridView ID="dgvReceive" runat="server" AllowPaging="false"
                                        AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="dgvReceive_RowDataBound">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                        Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCode1" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResdesc1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc3")) %>'></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotalRcv" runat="server" CssClass="btn btn-primary   primarygrdBtn" OnClick="lnkTotalRcv_Click">Total Calc</asp:LinkButton>

                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Specification" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpcdesc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="spcfcode" Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpcode" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'></asp:Label>
                                                </ItemTemplate>                                            
                                             <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvUnit1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linfunit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvordqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblgvFordqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Free Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgrvFreeqty1" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblgrvFFreeqty1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rec. Upto Last" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvreuptlast" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvuptolast")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblgvFreuptlast" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remaining Order" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrmainord" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainordr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="lblgvFrmainord" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive Qty" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvrcvQty" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                                        Style="text-align: right" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                                        BorderWidth="1px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFrcvQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lot No." HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvlotno" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotno")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expeire Date" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtexpeirdate" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Width="80px" Font-Size="12px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy ") %>'></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        TargetControlID="txtexpeirdate"></cc1:CalendarExtender>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkFinalUpdateR" runat="server" CssClass="btn btn-danger primaryBtn" 
                                                        OnClick="lnkFinalUpdateR_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                        </Columns>


                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <asp:Panel ID="pnlCosting" runat="server" Visible="False" TabIndex="22">
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="lbllcCost" runat="server" Visible="false" CssClass=" dataLblview" Text="Costing Details"></asp:Label>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </fieldset>

                                   <asp:GridView ID="gvlccost" runat="server" AllowPaging="false"
                                        AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                        Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCodelc" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                              
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotalLcCost" runat="server" CssClass="btn  btn-primary  primarygrdBtn" OnClick="lnkTotalLcCost_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResdesclc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            


                                             <asp:TemplateField HeaderText="Total L/C Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                
                                                  <FooterTemplate>
                                                    <asp:Label ID="lblgrvFtolcCost" runat="server" Font-Bold="True" 
                                                     Width="70px"   Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 
                                                 <ItemTemplate>
                                                    <asp:Label ID="lblgvtolcCost" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tolccost")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                 <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>



                                             <asp:TemplateField HeaderText=" Previous Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                
                                                  <FooterTemplate>
                                                    <asp:Label ID="lblgrvFprelcCost" runat="server" Font-Bold="True" 
                                                     Width="70px"   Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 
                                                 <ItemTemplate>
                                                    <asp:Label ID="lblgvprelcCost" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utorecamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                 <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                           
                                            
                                             <asp:TemplateField HeaderText="Received Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                
                                                  <FooterTemplate>
                                                    <asp:Label ID="lblgrvFcurlcCost" runat="server" Font-Bold="True" 
                                                     Width="70px"   Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 
                                                 <ItemTemplate>
                                                    <asp:TextBox ID="txtgvcurlcCostt" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px" BorderStyle="None" style="text-align:right;  background-color:transparent;"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                             <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>




                                             <asp:TemplateField HeaderText=" Balance" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                
                                                  <FooterTemplate>
                                                    <asp:Label ID="lblgrvFlcbalance" runat="server" Font-Bold="True" 
                                                     Width="70px"   Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                 
                                                 <ItemTemplate>
                                                    <asp:Label ID="lblgvlcbalance" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                 <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                           
                                        </Columns>


                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <div class="clearfix"></div>

                                
                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <asp:Panel ID="pnlexcelheading" runat="server" Visible="False" TabIndex="22">


                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="lblheding" runat="server" CssClass=" dataLblview" Text="Product Details"></asp:Label>


                                                    </div>




                                                </div>




                                            </asp:Panel>

                                        </div>
                                    </fieldset>


                                    <asp:Repeater ID="rpprodetails" runat="server">

                                        <HeaderTemplate>
                                            <table id="tblrpprodetails" class=" table-striped table-hover table-bordered grvContentarea">
                                                <tr>
                                                    <th>SL</th>
                                                    <th>Product_Id</th>
                                                    <th>Pack_No</th>
                                                    <th>M_IMEI</th>
                                                    <th>S_IMEI</th>
                                                    <th>Serial_No</th>
                                                    <th>Color</th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblrpSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lrpproid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Product_Id")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lblrppackno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Pack_No")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpmimei" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "M_IMEI")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpsimei" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "S_IMEI")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpselno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Serial_No")) %>'
                                                        Width="110px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblrpColor" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Color")) %>'
                                                        Width="100px"></asp:Label>
                                                </td>

                                            </tr>

                                        </ItemTemplate>

                                        <FooterTemplate>

                                            <tr>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>

                                                <th></th>
                                            </tr>


                                            </table>
                                        </FooterTemplate>





                                    </asp:Repeater>
                            </asp:View>
                            <asp:View ID="ViewCosting" runat="server">
                                <fieldset class="scheduler-border fieldset_B">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">



                                                <div class="msgHandSt">
                                                    <asp:Label ID="lblmsg1" runat="server" CssClass="btn-danger btn primaryBtn" Visible="false"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                                <div class="table-responsive">
                                    <asp:GridView ID="dgvCosting" runat="server" AllowPaging="False" OnRowDeleting="dgvOrder_RowDeleting"
                                        AutoGenerateColumns="False" ShowFooter="true"
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                        Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                                ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCode2" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Resource Description"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResdesc2" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrvUnit2" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrcvqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Rate(FC)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvratefc" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Convertion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCon" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conve")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="FC Amt in BDT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvamtfc" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amtfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvftfcamt" runat="server" Font-Size="12px" Font-Bold="True" ForeColor="#CC0066"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Local Payment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvamtdpcar" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amtdp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvftdpamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtoamt" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvftToamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="FC Rate in BDT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrateBD" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratbd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Local Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvratedpcar" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratedp")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Rate" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTotalrat" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratetotal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                        </Columns>

                                        <FooterStyle BackColor="#F5F5F5" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>

                                    <fieldset class="scheduler-border fieldset_B">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="lnkRateupdate" runat="server" OnClick="lnkRateupdate_Click" CssClass="btn btn-danger  primarygrdBtn">Rate Update</asp:LinkButton>

                                                    </div>

                                                </div>

                                                <div class="col-md-3 pading5px asitCol3">
                                                    <div class="colMdbtn">
                                                        <asp:LinkButton ID="lnkovrallupdate" runat="server" OnClick="lnkovrallupdate_Click" CssClass="btn btn-danger  primarygrdBtn">Overall Update</asp:LinkButton>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                            </asp:View>
                        </asp:MultiView>
                        <div>
                            <asp:Label ID="lblPrintMsg" runat="server" CssClass="FormLevel"></asp:Label>
                            <%--<cc1:RoundedCornersExtender ID="Roundeender2" runat="server" BorderColor="PowderBlue"
                                Color="PowderBlue" Radius="7" TargetControlID="Panel4">
                            </cc1:RoundedCornersExtender>
                            <cc1:RoundedCornersExtender ID="Roundeender3" runat="server" BorderColor="PowderBlue"
                                Color="PowderBlue" Radius="7" TargetControlID="Panel5">
                            </cc1:RoundedCornersExtender>
                            <cc1:RoundedCornersExtender ID="Roundeender4" runat="server" BorderColor="PowderBlue"
                                Color="PowderBlue" Radius="7" TargetControlID="Panel6">
                            </cc1:RoundedCornersExtender>--%>
                        </div>

                    </div>
            </div>

            <%--<div class="row">
                <fieldset class="scheduler-border fieldset_A">

                    <div class="form-horizontal">
                        <div class="form-group">

                            <div class="formBtn ">

                                <div class="pull-right">
                                   <<asp:LinkButton ID="lnkbtnSaveSupl" runat="server" <asp:LinkButton ID="btnClose" runat="server" CssClass="btn  btn-primary primaryBtn pull-right " Style="margin: 0 5px;"  ><span class="flaticon-delete47 text-danger "></span>Close</asp:LinkButton> CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;"><i class="fa fa-floppy-o text-primary"></i> Save</asp:LinkButton>
                                   <asp:LinkButton ID="btnClose" runat="server" CssClass="btn  btn-primary primaryBtn text-danger" OnClick="btnClose_Click" Style="margin: 0 5px;"><i class="fa fa-times text-danger"></i>Close</asp:LinkButton>

                                    <asp:HyperLink ID="lnkbtnAdd" runat="server" CssClass="btn  btn-primary primaryBtn"Style="margin: 0 5px;"  NavigateUrl="~/F_17_Acc/AccInv.aspx">Add</asp:HyperLink>
                                </div>
                            </div>





                        </div>



                    </div>
                </fieldset>
            </div>--%>
           

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
