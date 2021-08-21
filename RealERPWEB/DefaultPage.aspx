<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="DefaultPage.aspx.cs" Inherits="RealERPWEB.DefaultPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .margin10pxTop {
            margin-top: 10px;
        }

        .nf {
            margin-top: 5px !important;
            font-size: 25px !important;
            color: #175374 !important;
        }

        /*-----------------------------------*/

        /*--------------------------------------------*/



        .allpagebg {
            background-image: url("Image/bg.PNG") !important;
            overflow: hidden;
        }

        .glyph {
            display: inline-block;
            width: 120px;
            margin: 10px;
            text-align: center;
            vertical-align: top;
            background: #FFF;
        }

            .glyph .glyph-icon {
                padding: 10px;
                display: block;
                font-family: "Flaticon";
                font-size: 64px;
                line-height: 1;
            }

                .glyph .glyph-icon:before {
                    font-size: 64px;
                    color: #666;
                    margin-left: 0;
                }

        .flowMenu2 [class^="flaticon-"]::before, .flowMenu2 [class*=" flaticon-"]::before, .flowMenu2 [class^="flaticon-"]::after, .flowMenu2 [class*=" flaticon-"]::after {
            /*border: 1px solid #a1eaea;*/
            color: #008000 !important;
            display: block;
            font-family: Flaticon;
            font-size: 23px !important;
            font-style: normal;
            margin-top: 5px !important;
            padding: 5px 2px !important;
        }

        .nfont {
            font-size: 22px !important;
            color: green;
        }

        .nfont1 {
            font-size: 22px !important;
            color: #666666;
        }

        .nfontColor1 {
            font-size: 22px !important;
            color: #990033;
            /*color:#0E7FBE;
     opacity:0.5;*/
        }

        .nfontColor2 {
            font-size: 22px !important;
            color: green;
            /*color:#E26226;
     opacity:0.5;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function loadModal() {

            $('#exampleModal').modal('show');
        }
    </script>
    <div class="container  moduleItemWrp defaultMenuPart ">
        <div class="row">
            <div class="col-sm-2 col-md-2 col-lg-2 mfgacc2">
                <div class="pnlInterface">
                    <asp:Panel ID="pnlIntrbudget" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("~/F_08_PPlan/ConstructionInfo.aspx")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>


                        <ul class="sideMenu ">
                            <li class="divSMenu1"></li>

                            <li class="divSMenu2"></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubCodeBook.aspx?InputType=Materials")%>" target="_blank">Materials</a> </li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubCodeBook.aspx?InputType=Labour")%>" target="_blank">Labour</a> </li>

                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubCodeBook.aspx?InputType=Wrkschedule")%>" target="_blank">Work Schedule</a> </li>
                            <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdStdAna.aspx")%>" target="_Self">Standard Analysis</a> </li>


                        </ul>

                    </asp:Panel>
                    <asp:Panel ID="pnlintrland" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("#")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                    </asp:Panel>

                    <asp:Panel ID="pnlintrConst" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("~/F_08_PPlan/ConstructionInfo.aspx")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                    </asp:Panel>
                    <asp:Panel ID="pnlintrsales" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("~/F_22_Sal/SalesInformation.aspx")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                    </asp:Panel>
                    <asp:Panel ID="plnintrcr" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("#")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                    </asp:Panel>
                    <asp:Panel ID="pnlintrModification" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("#")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                    </asp:Panel>
                    <asp:Panel ID="pnlintrpur" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                        <ul class="sideMenu ">
                            <li class="divSMenu1"></li>

                            <li class="divSMenu2"></li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubCodeBook.aspx?InputType=Materials")%>" target="_blank">Materials</a> </li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubCodeBook.aspx?InputType=ResCodePrint")%>" target="_blank">Supplier Code</a> </li>

                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurSupplierinfo.aspx")%>" target="_blank">Supplier Information</a> </li>
                            <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSpecificCodeBook.aspx")%>" target="_blank">Specefication Code</a> </li>

                            <li><a href="<%=this.ResolveUrl("~/F_14_Pro/PurMktSurvey.aspx?Type=SurveyLink")%>" target="_blank">Survey Link</a> </li>
                        </ul>

                    </asp:Panel>
                    <asp:Panel ID="pnlintrInv" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation.aspx")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                    </asp:Panel>
                    <asp:Panel ID="pnlintrAcc" Visible="false" runat="server">
                        <a class="nonBg" href="<%=this.ResolveUrl("~/F_18_MAcc/AccDashBoard.aspx")%>" target="_blank">
                            <img src="Image/int1.png" /><span class="interFaceMenu">Interface</span></a>

                    </asp:Panel>



                </div>

                <%--<ul class="sideMenu ">
                    <li class="divSMenu1"></li>

                    <li class="divSMenu2"></li>
                    <li>
                        <asp:LinkButton ID="lblGrp" runat="server" OnClick="lblGrp_Click"> Profile</asp:LinkButton></li>
                    <li><a href="" data-toggle="modal" data-target=".bannerformmodal">About</a></li>
                    <li><a href="<%=this.ResolveUrl("#")%>" target="_Self">Exit</a>


                    </li>

                </ul>--%>
            </div>

            <div class="col-sm-10 col-md-10 col-lg-10 flowMenu2" style="padding: 0px !important;">


                <asp:Panel ID="landpanal" runat="server">


                    <div class="margin10pxTop">



                        <div class="col-md-12">

                            <div class="intrmMenu"></div>


                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-buildings8"></span>
                                    </div>

                                    <div class="intermChMenub  ">
                                        L. Acquisition

                                    </div>
                                </div>

                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_01_LPA/LpSCodeBook.aspx?BookName=Project")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-badges1"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Create New Offer


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("#")%>" target="_blank">
                                    <div class="dirrWrpUD">
                                        <i class="fa fa-long-arrow-down nfont"></i>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>


                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <%-- <a  href="<%=this.ResolveUrl("~/#")%>" target="_Self">
                                                <div class="intrmChMenu">
                                                    <div class="interImgM">
                                                        <img src="Image/PF.png" alt="img" style="width: 25px; height: 28px;" />
                                                    </div>

                                                    <div class="intermChMenub">
                                                        purchase

                                                    </div>
                                                </div>
                                            </a>--%>
                            </div>

                            <div class="dirrWrp">
                                <%--<div class="dirrWrpLR">
                                                <img src="Image/L1.png" alt="img" />
                                            </div>--%>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_01_LPA/PriLandProposal.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-label9"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            1st Approval 


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_01_LPA/PriLandProposal.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-settings38"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Initial data bank 

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("#")%>" target="_blank">
                                    <div class="dirrWrpUD">
                                        <i class="fa fa-long-arrow-down nfont"></i>
                                    </div>
                                </a>
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <%--  <a  href="<%=this.ResolveUrl("~/F_01_LPA/LandDevProposal.aspx?Type=LandEntry")%>" target="_Self">
                                                <div class="intrmChMenu">
                                                    <div class="interImgM">
                                                        <img src="Image/PF.png" alt="img" style="width: 25px; height: 28px;" />
                                                    </div>

                                                    <div class="intermChMenub ">
                                                       Project Information


                                                    </div>
                                                </div>
                                            </a>--%>
                            </div>

                            <div class="dirrWrp">
                                <%-- <div class="dirrWrpLR">
                                                <img src="Image/R1.png" alt="img" />
                                            </div>--%>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_01_LPA/LandDevProposal.aspx?Type=LandEntry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-building150"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Sensitivity Analysis 	


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_01_LPA/LandDevProposal.aspx?Type=LandEntry")%>" target="_blank">

                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-bars-graphic"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Feasibility


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_01_LPA/RptLandDevTopSheet.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-architecture10"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Report for decision


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>




                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("#")%>" target="_blank">
                                    <div class="dirrWrpUD">
                                        <i class="fa fa-long-arrow-down nfont"></i>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>


                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_01_LPA/RptAllProTopSheet.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-cogwheel28"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Data Bank
                                        </div>
                                    </div>
                                </a>
                                <%-- <a  href="<%=this.ResolveUrl("~/F_01_LPA/RptLandDevTopSheet.aspx")%>" target="_Self">
                                                <div class="intrmChMenu">
                                                    <div class="interImgM">
                                                        <img src="Image/icon41.png" alt="img" style="width: 25px; height: 28px;" />
                                                    </div>

                                                    <div class="intermChMenub sendline">
                                                       3 Result for <br />decision making


                                                    </div>
                                                </div>
                                            </a>--%>
                            </div>
                            <div class="dirrWrp">
                                <%-- <div class="dirrWrpLR">
                                                <img src="Image/R1.png" alt="img" />
                                            </div>--%>
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                        </div>


                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>
                    </div>


                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("~/F_01_LPA/RptLandDevProposal.aspx?Type=Revenue")%>" target="_blank"><span class=""></span>Revenue Details   </a></li>
                                <%--glyphicon glyphicon-unchecked--%>
                                <li><a href="<%=this.ResolveUrl("~/F_01_LPA/RptLandDevProposal.aspx?Type=Cost")%>" target="_blank"><span class=""></span>Cost Details</a></li>

                            </ul>
                        </div>
                    </div>

                    <div class="clearfix"></div>

                </asp:Panel>

                <asp:Panel ID="bgpanal" Visible="false" runat="server">

                    <div class="margin10pxTop">

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_08_PPlan/PrjCompFlowchart.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-tray30"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Pre-Construction Plan
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu ">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-budget"></span>
                                    </div>

                                    <div class="intermChMenub ">
                                        Budget

                                    </div>
                                </div>

                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/AccProjectCode.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-badges1"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Create New Project


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>


                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/MktEntryUnit.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-file294"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Revenue  Budget


                                        </div>
                                    </div>
                                </a>

                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/PrjInformation.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-information58"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Project Information


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMain")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-budget1"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Contruction Budget


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-computer218"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Income Statement
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMain")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-graph4"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Copy Related One
                                        </div>
                                    </div>
                                </a>

                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-up nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>


                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdMaster.aspx?InputType=BgdMain")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-currency13"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            General Budget


                                        </div>
                                    </div>
                                </a>

                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_08_PPlan/ProTargetTimeBasis.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-flow5"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Contruction  Planing


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdLevelRate.aspx?Type=Level")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-computer118"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Contruction Level 


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Report</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgdAcWk")%>" target="_blank"><span class=""></span>01. Budgeted Income Statement -(Work)</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/RptBgdPrjoject.aspx?Type=MasterBgd")%>" target="_blank"><span class=""></span>02. Budgeted Income Statement -(Resource)</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/RptBgdPrjoject.aspx?Type=WrkVsResource")%>" target="_blank"><span class=""></span>03. Budgeted Work Vs. Resource</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=2")%>" target="_blank"><span class=""></span>07. Individual Material Details</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdPrjAna.aspx?InputType=BgdMainRpt&AnaType=3")%>" target="_blank"><span class=""></span>08. Individual Work Details</a></li>


                            </ul>

                        </div>
                    </div>

                    <div class="clearfix"></div>

                </asp:Panel>

                <asp:Panel ID="pnlConst" runat="server" Visible="false">

                    <div class="margin10pxTop">

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/BgdLevelRate.aspx?Type=Rate")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-working9"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Sub-Contructor Rate 

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-hammer24"></span>
                                    </div>

                                    <div class="intermChMenub ">
                                        Construction
                                    </div>
                                </div>

                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>


                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_09_PImp/ImplementPlan.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-strategical"></span>
                                        </div>

                                        <div class="intermChMenub sendline ">
                                            System G.Target

                                        </div>
                                    </div>
                                </a>

                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_09_PImp/ImplementPlan.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-business73"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Revised Target


                                        </div>
                                    </div>
                                </a>

                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>



                        </div>

                        <%--<div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>--%>

                        <%--<div class="col-md-12">
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/RptPrurVarAna.aspx?Type=StkBasis")%>" target="_Self">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-tablet38"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Stock Report

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry")%>" target="_Self">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-software7"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Materials Requisition
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurMRREntry.aspx?Type=Entry")%>" target="_Self">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-truck29"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Materials Receive

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurMatIssue.aspx?Type=Entry")%>" target="_Self">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-callcenter"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Materials Issue
                                        </div>
                                    </div>
                                </a>

                            </div>
                        </div>--%>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_32_Mis/RptConstruProgress.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-bars-graphic"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Contruction Progress


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_09_PImp/PurIssueEntry.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-construction10"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Work Execution

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_09_PImp/PurLabIssue.aspx?Type=Current")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-contract11"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Sub-Contructor Bill


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <%--  <i class="fa fa-long-arrow-right nfont"></i>--%>
                                </div>
                            </div>
                            <div class="intrmMenu">
                                <%-- <a href="<%=this.ResolveUrl("~/F_09_PImp/SubConBillEntry.aspx")%>" target="_Self">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-creditcard7"></span>
                                        </div>

                                        <div class="intermChMenub">
                                            Bill Payment
                                        </div>
                                    </div>
                                </a>--%>
                            </div>
                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_14_Pro/RptMatPurHistory.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-book115"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Materials History


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-bulb16"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Inflation Effect

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>


                    </div>



                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/RptPrurVarAna.aspx?Type=StkBasis")%>" target="_blank"><span class=""></span>Stock Report </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry&prjcode=&genno=")%>" target="_blank"><span class=""></span>Materials Requisition</a></li>
                                <%--glyphicon glyphicon-unchecked--%>

                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMRREntry.aspx?Type=Entry")%>" target="_blank"><span class=""></span>Materials Receive</a></li>
                                <%--glyphicon glyphicon-unchecked--%>
                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/PurMatIssue.aspx?Type=Entry")%>" target="_blank"><span class=""></span>Materials Issue</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_09_PImp/SubConBillEntry.aspx")%>" target="_blank"><span class=""></span>Bill Payment</a></li>


                            </ul>

                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel ID="pnlsales" runat="server" Visible="false">

                    <div class="margin10pxTop">

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/MonthlySalesBudget.aspx?Type=Monthly")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-business73"></span>
                                        </div>

                                        <div class="intermChMenub   fncolor">
                                            Sales Target

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-house118"></span>
                                    </div>

                                    <div class="intermChMenub ">
                                        Sales

                                    </div>
                                </div>

                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>


                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_21_Mkt/RptMktAppointment.aspx?Type=Todaysdis&UType=Mgt")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-register"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Today's Status

                                        </div>
                                    </div>
                                </a>
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>


                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_21_Mkt/RptProWiseClOffered.aspx?Type=SalesDeci")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-sale18"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Sales Decision
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-rihgt nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_21_Mkt/ToDaysAppointment.aspx?Type=ClDiscuss&UType=Client")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-panel3"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Clients Discussion

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>


                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_21_Mkt/RptMktAppointment.aspx?Type=DiscussHis&UType=Mgt")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-folder294"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Client History
                                        </div>
                                    </div>
                                </a>
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>


                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=CustNoteSheet")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-sale5"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Sales Approval

                                        </div>
                                    </div>
                                </a>

                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/MktSalsPayment.aspx?Type=Sales")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-plan2"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Payment Schedule

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_23_CR/MktMoneyReceipt.aspx?Type=CustCare")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-searching26"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Collection

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/AcceessError.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-pencil41"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Agreement
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=CustApp")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-architecture2"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Allotment Letter
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccSalJournal.aspx?Type=Consolidate")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-data62"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Advance Sales Update
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu">
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                    </div>



                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=RptDayWSale")%>" target="_blank"><span class=""></span>Day Wise Sales </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptSaleSoldunsoldUnit.aspx?Type=soldunsold")%>" target="_blank"><span class=""></span>Sold & Unslod Information </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_22_Sal/RptAvailChart.aspx?Type=Details")%>" target="_blank"><span class=""></span>Availability Chart 1</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_23_CR/RptCustPayStatus.aspx?Type=ClLedger")%>" target="_blank"><span class=""></span>Client Ledger </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_23_CR/RptCustomerDues.aspx")%>" target="_blank"><span class=""></span>Customer Dues Information</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_23_CR/RptReceivedList02.aspx?Type=DuesCollect")%>" target="_blank"><span class=""></span>Dues Collection Statment</a></li>

                            </ul>

                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel ID="plncr" runat="server">

                    <div class="margin10pxTop">



                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/MonthlySalesBudget.aspx?Type=Monthly")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-stocks3"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Collection Target

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-coins18"></span>
                                    </div>

                                    <div class="intermChMenub">
                                        C.Realization
                                    </div>
                                </div>

                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>


                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_21_Mkt/RptMktAppointment.aspx?Type=Todaysdis&UType=Mgt")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-register"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Today's Status

                                        </div>
                                    </div>
                                </a>
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>


                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/RptThanksLetter.aspx?Type=Remind")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-customerservice26"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            1st Reminder


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=DueCollAll")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-wiring"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Invoice Send

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>


                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_23_CR/MktMoneyReceipt.aspx?Type=CustCare")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-magnifyingglass27"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Collection

                                        </div>
                                    </div>
                                </a>
                            </div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/RptThanksLetter.aspx?Type=LRemind")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-light105"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            2nd Reminder

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/CodeDataTrans.aspx?Type=CodeTransfer")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-zoom69"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Cancellation

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccChqueDeposit.aspx?Type=ChquedepEntry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-creditcard7"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Cheque Deposit

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_23_CR/RptCustPayStatus.aspx?Type=ClLedger")%>" target="_Self">
                                    <div class="intrmChMenu ">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-computer218"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Client Ledger (B/A)

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccSales.aspx")%>" target="_Self">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-telephone172"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Collection update

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/RptMktMoneyReceipt.aspx?Type=Management")%>" target="_Self">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-searching26"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Cancelled Cheque

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                    </div>



                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("#")%>" target="_blank"><span class=""></span>Recurring Task </a></li>

                                <li><a href="<%=this.ResolveUrl("#")%>" target="_blank"><span class=""></span>Item-025   </a></li>


                            </ul>

                        </div>
                    </div>




                </asp:Panel>

                <asp:Panel ID="pnlModification" runat="server" Visible="false">

                    <div class="margin10pxTop">


                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_24_CC/RptLoanStatus.aspx?Type=Letter")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-software3"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Correspondence
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-tool879"></span>
                                    </div>

                                    <div class="intermChMenub">
                                        Modification

                                    </div>
                                </div>

                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>


                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_24_CC/CustMaintenanceWork.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-working9"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Additional Work


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_24_CC/CompanyStandardChoice.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-international36"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Project Standard


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_24_CC/CustMaintenanceWork.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-report1"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Omitting Work Bill


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccSalesADandDelay.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-telephone172"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Accounts Update

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/MktSalsPayment.aspx?Type=Sales")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-graph22"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Add with schedule

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_04_Bgd/AddBudget.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-budget"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Additional Budget 

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-badges1"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Create Requisition 

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>
                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_25_Reg/EntryRegclearacne.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-news35"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Registration

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/CodeTransSupSub.aspx?Type=General")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-coins18"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Refund

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/MktSalsPayment.aspx?Type=Sales")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-checkmark11"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Adjustment schedule


                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/SuplierPaymentPost.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-architecture10"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Create Payable


                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>
                            <div class="intrmMenu"></div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                    </div>







                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("#")%>" target="_blank"><span class=""></span>Recurring Task </a></li>

                                <li><a href="<%=this.ResolveUrl("#")%>" target="_blank"><span class=""></span>Item-025   </a></li>


                            </ul>

                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel ID="pnlInv" runat="server">

                    <div class="margin10pxTop">


                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-cash4"></span>
                                    </div>

                                    <div class="intermChMenub  ">
                                        Inventroy
                                    </div>
                                </div>

                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_14_Pro/RptMatPurHistory.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-research1"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Material History
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-badges1"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Create Requistion
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=IndSup")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-checkmark11"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Supplier History
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("#")%>" target="_Self">
                                    <div class="dirrWrpUD">
                                        <i class="fa fa-long-arrow-down nfont"></i>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>



                        </div>


                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/MaterialsTransfer.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-construction12"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Material Transfer
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurMRREntry.aspx?Type=Entry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-architecture10"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Materials Receive
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurMatIssue.aspx?Type=Entry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-callcenter"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Material Issue
                                        </div>
                                    </div>
                                </a>

                            </div>



                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <a href="<%=this.ResolveUrl("~/F_12_Inv/RptPrurVarAna.aspx?Type=StkBasis")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-money296"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Stock Report

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                 <a href="<%=this.ResolveUrl("~//F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-money296"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                           MRR Search

                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur")%>" target="_blank"><span class=""></span>Day Wise Purchase</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum")%>" target="_blank"><span class=""></span>Purchase Summary</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=Purchasetrk")%>" target="_blank"><span class=""></span>Purchase Tracking-01</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptMatPurHistory.aspx")%>" target="_blank"><span class=""></span>Purchase History-Supplier Wise</a></li>


                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/RptProjectStock.aspx?Type=inv")%>" target="_blank"><span class=""></span>Materials Stock Information(Inventory)</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/RptMatIssueStatus.aspx")%>" target="_blank"><span class=""></span>Materials Issue Status </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/MatTransStatus.aspx")%>" target="_blank"><span class=""></span>Material Transfer Status</a></li>

                                <li><a href="<%=this.ResolveUrl("~/AcceessError.aspx")%>" target="_blank"><span class=""></span>Materials Stock Information(Project Wise)</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_12_Inv/RptProjectStock.aspx?Type=acc")%>" target="_blank"><span class=""></span>Materials Stock Information</a></li>



                            </ul>

                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlpur" Visible="false">




                    <div class="margin10pxTop">


                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor">
                                    <div class="interImgM ">
                                        <span class="glyph-icon flaticon-favourite26"></span>
                                    </div>

                                    <div class="intermChMenub  ">
                                        Purchase
                                    </div>
                                </div>

                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccSubCodeBook.aspx?InputType=ResCodePrint")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-badges1"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Create New Creditor

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("#")%>" target="_Self">
                                    <div class="dirrWrpUD">
                                        <i class="fa fa-long-arrow-down nfont"></i>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=RateInput&prjcode=&genno=")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-speechbubbles3"></span>


                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Rate Proposal
                                        </div>
                                    </div>
                                </a>
                            </div>


                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqEntry.aspx?InputType=Entry&genno=")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-list23"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Requisition  List
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_14_Pro/PurMktSurvey.aspx?Type=MktSurvey")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-newspaper21"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            CS Preparation
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <%--  <div class="dirrWrpLR">
                                    <img src="Image/L1.png" alt="img" />
                                </div>--%>
                            </div>

                            <div class="intrmMenu">

                                <%--   <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger.aspx?Type=ASPayment")%>">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <img src="Image/10.jpg" alt="img" style="width: 25px; height: 28px;" />
                                        </div>

                                        <div class="intermChMenub sendline">
                                            Supplier Overall Position
                                        </div>
                                    </div>
                                </a>
                            </div>--%>
                            </div>
                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("#")%>" target="_Self">
                                    <div class="dirrWrpUD">
                                        <i class="fa fa-long-arrow-down nfont"></i>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_12_Inv/PurReqApproval.aspx?Type=Approval&prjcode=&genno=")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-currency22"></span>
                                        </div>

                                        <div class="intermChMenub">
                                            Approval
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_14_Pro/PurAprovEntry.aspx?InputType=PurProposal&genno=")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-game50"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Order Process 
                                        </div>
                                    </div>
                                </a>

                            </div>



                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("#")%>" target="_Self">
                                    <div class="dirrWrpUD">
                                        <i class="fa fa-long-arrow-down nfont"></i>
                                    </div>
                                </a>
                            </div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <%-- <i class="fa fa-long-arrow-left nfont"></i>--%>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <a href="<%=this.ResolveUrl("~/F_14_Pro/PurBillEntry.aspx?Type=BillEntry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-game50"></span>
                                        </div>

                                        <div class="intermChMenub  fncolor">
                                            Purchase Invoice
                                        </div>
                                    </div>
                                </a>

                                <%--  <a href="<%=this.ResolveUrl("~/F_12_Inv/PurMRREntry.aspx?Type=Entry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-game50"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Material Received

                                        </div>
                                    </div>
                                </a>--%>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_14_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry&genno=")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-pencil41"></span>
                                        </div>

                                        <div class="intermChMenub fncolor ">
                                            Purchase Order

                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>

                        <%-- <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont"></i>
                                </div>
                            </div>

                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>
                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccPurchase.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-telephone172"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Update Purchase
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger.aspx?Type=SPayment02")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-data45"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Statment of Accounts

                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfont"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-coins18"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Payment 

                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>--%>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>
                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                    </div>






                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptRequsitionStatus.aspx?WType=ReqStatus&Type=Purchase")%>" target="_blank"><span class=""></span>Requisition Status</a></li>
                                <%--glyphicon glyphicon-unchecked--%>
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptWorkOrderStatus.aspx?Type=DetailsWorkIOrdStatus")%>" target="_blank"><span class=""></span>Work Order Details </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptWorkOrderVsSupply.aspx?Type=OrdVsSup")%>" target="_blank"><span class=""></span>Work Order-Supplier Wise</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptWorkOrderHistorySup.aspx?Type=WorkOrdHisSup")%>" target="_blank"><span class="">Work Order Histroy Supliers</span> </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=BgdBal")%>" target="_blank"><span class=""></span>Budget Tracking
                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=Purchasetrk")%>" target="_blank"><span class=""></span>Purchase Tracking-01
                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur")%>" target="_blank"><span class=""></span>Day Wise Purchase
                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/SupProposeBill.aspx")%>" target="_blank"><span class=""></span>Supplier Proposed Payment
                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger.aspx?Type=ASPayment")%>" target="_blank"><span class=""></span>Supplier Overall Position
                                </a></li>


                            </ul>
                        </div>
                    </div>
                    <div class="clearfix"></div>

                </asp:Panel>

                <asp:Panel runat="server" ID="pnlAcc" Visible="false">

                    <div class="margin10pxTop">

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit%20Voucher&Mod=Accounts")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-computerscreen27"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            General Receive
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-credit56"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            General Payment

                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                </div>
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-up nfontColor2"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-up nfontColor1"></i>
                                </div>

                            </div>



                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu BgColor lgMenu">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-copy32"></span>

                                    </div>

                                    <div class="intermChMenub  ">
                                        Receive
                                    </div>
                                </div>

                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfontColor2"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <div class="intrmChMenu lgMenu BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-settings49"></span>
                                    </div>

                                    <div class="intermChMenub">
                                        Accounts
                                    </div>
                                </div>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfontColor1"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <div class="intrmChMenu  BgColor">
                                    <div class="interImgM">
                                        <span class="glyph-icon flaticon-money13"></span>
                                    </div>

                                    <div class="intermChMenub">
                                        Payment
                                    </div>
                                </div>

                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfontColor1"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/SuplierPaymentPost.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-calculator69"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Pay Bill-PDC
                                        </div>
                                    </div>
                                </a>
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfontColor2"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont1"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfontColor1"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfontColor1"></i>
                                </div>

                            </div>




                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_23_CR/RptCustPayStatus.aspx?Type=ClLedger")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-construction10"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Client Ledger-Process
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont1"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">

                                <a href="<%=this.ResolveUrl("~/F_23_CR/MktMoneyReceipt.aspx?Type=CustCare")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-sales3"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Receive From Sales
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfontColor2"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal%20Voucher&Mod=Accounts")%>" target="_blank">
                                    <div class="intrmChMenu BgColor">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-levels1"></span>
                                        </div>

                                        <div class="intermChMenub">
                                            Journal
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left  nfontColor1"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/SuplierPayment.aspx?tcode=99&tname=Payment%20Voucher&Mod=Accounts")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-money232"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Pay Bills                                   
                                        </div>
                                    </div>
                                </a>

                            </div>



                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccPayUpdate.aspx?Type=AccIsu")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-panel3"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            PDC Up-Date                            
                                        </div>
                                    </div>
                                </a>
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfontColor2"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu"></div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfontColor1"></i>
                                </div>
                            </div>
                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfontColor1"></i>
                                </div>
                            </div>


                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_23_CR/CustChDishoner.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-mastercard4"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Cheque Dishonour
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfont1"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccChqueDeposit.aspx?Type=ChquedepEntry")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-money405"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Cheque Deposit
                                        </div>
                                    </div>
                                </a>

                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfontColor2"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_23_CR/RptCustPayStatus.aspx?Type=ClLedger")%>" target="_blank">
                                    <div class="intrmChMenu  ">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-numbered10"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Client Ledger                             
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-right nfontColor2"></i>
                                </div>
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccTrialBalance.aspx?Type=Mains")%>" target="_blank">
                                    <div class="intrmChMenu ">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-sales4"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Trial Balance                         
                                        </div>
                                    </div>
                                </a>

                            </div>



                            <div class="dirrWrp">
                                <div class="dirrWrpLR">
                                    <i class="fa fa-long-arrow-left nfontColor1"></i>
                                </div>
                            </div>
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger.aspx?Type=DetailLedger")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-plan2"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Supplier  Ledger 
                                        </div>
                                    </div>
                                </a>
                            </div>

                        </div>

                        <div class="col-md-12">

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont1"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont1"></i>
                                </div>


                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">

                                <div class="dirrWrpUD">
                                    <i class="fa fa-long-arrow-down nfont1"></i>
                                </div>

                            </div>

                            <div class="dirrWrp"></div>

                            <div class="intrmMenu">
                            </div>



                        </div>

                        <div class="col-md-12">
                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_22_Sal/RptSalInterest.aspx?Type=interest")%>" target="_blank">
                                    <div class="intrmChMenu ">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-prize3"></span>
                                        </div>

                                        <div class="intermChMenub  ">
                                            Create Charge
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/F_17_Acc/AccSales.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-computerscreen27"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Receive Confirmation                                 
                                        </div>
                                    </div>
                                </a>

                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/AcceessError.aspx")%>" target="_blank">
                                    <div class="intrmChMenu">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-computerscreen27"></span>
                                        </div>

                                        <div class="intermChMenub fncolor">
                                            Sub-Conductor Payment                              
                                        </div>
                                    </div>
                                </a>
                            </div>

                            <div class="dirrWrp">
                            </div>

                            <div class="intrmMenu">
                                <a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=22")%>" target="_blank">
                                    <div class="intrmChMenu ">
                                        <div class="interImgM">
                                            <span class="glyph-icon flaticon-compressed2"></span>
                                        </div>

                                        <div class="intermChMenub ">
                                            Financial  Statement 
                                        </div>
                                    </div>
                                </a>

                            </div>



                            <div class="dirrWrp">
                            </div>
                            <div class="intrmMenu">
                            </div>

                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-12 relatedItems ">
                            <h3>Related Items</h3>


                            <ul class="nav-pills">
                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccPurchase.aspx")%>" target="_blank"><span class=""></span>Purchase Update
                                </a></li>
                                <%--glyphicon glyphicon-unchecked--%>
                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSalJournal.aspx?Type=Consolidate")%>" target="_blank"><span class=""></span>Sales Update</a></li>
                                <%-- <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccSales.aspx")%>" target="_blank"><span class=""></span>Collection Update</a></li>--%>
                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccConBillUpdate.aspx")%>" target="_blank"><span class=""></span>Sub-Contractor Bill </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=RecPay")%>" target="_blank"><span class=""></span>Receipts & Payment (Honoured)</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/TransectionPrint.aspx?Type=AccVoucher&Mod=Accounts")%>" target="_blank"><span class=""></span>Voucher Print</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_32_Mis/ProjReport02.aspx")%>" target="_blank"><span class=""> </span>Project Report</a></li>


                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/AccLedger.aspx?Type=Ledger&RType=GLedger")%>" target="_blank"><span class=""></span>Ledger-01</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccAITVATASDAllSup.aspx")%>" target="_blank"><span class=""></span>AIT, VAT & SD Dedecution</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_17_Acc/RptAccSpLedger.aspx?Type=ASPayment")%>" target="_blank"><span class=""></span>Supplier Overall Position</a></li>


                            </ul>

                        </div>
                    </div>


                    <div class="clearfix"></div>


                </asp:Panel>


            </div>

            <div class="modal fade AsitModal profModl" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog row">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close btn-danger" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>




                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">

                                <ContentTemplate>
                                    <div class="toppad">


                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Profile</h3>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">

                                                    <!--<div class="col-xs-10 col-sm-10 hidden-md hidden-lg"> <br>
                  <dl>
                    <dt>DEPARTMENT:</dt>
                    <dd>Administrator</dd>
                    <dt>HIRE DATE</dt>
                    <dd>11/12/2013</dd>
                    <dt>DATE OF BIRTH</dt>
                       <dd>11/12/2013</dd>
                    <dt>GENDER</dt>
                    <dd>Male</dd>
                  </dl>
                </div>-->
                                                    <div class=" col-md-12 col-lg-12 ">
                                                        <table class="table table-user-information grvContentareaProfile ">
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" CssClass="prolbl" Text="Company Name:"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblCompName" runat="server" Text="ASICOM PTE LTD"></asp:Label></td>
                                                                </tr>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" CssClass="prolbl" Text="Address:"></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblAddress" runat="server" Text="ASICOM PTE LTD"></asp:Label></td>
                                                                </tr>
                     
                   
                       
                         <tr>
                             <td>
                                 <asp:Label ID="Label3" runat="server" CssClass="prolbl" Text="Phone No. :"></asp:Label></td>
                             <td>
                                 <asp:Label ID="lblphone1" runat="server" Text="ASICOM PTE LTD"></asp:Label>
                             </td>

                         </tr>


                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label4" CssClass="prolbl" runat="server" Text="Email :"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblemail" runat="server" Text="ASICOM PTE LTD"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label5" CssClass="prolbl" runat="server" Text="Website :"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblweb" runat="server" Text="ASICOM PTE LTD"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label6" runat="server" CssClass="prolbl" Text="Accounting Period:"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblAccPeriod" runat="server" Text="ASICOM PTE LTD"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label7" runat="server" CssClass="prolbl" Text="License Status :"></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lbllicense" runat="server" Text="Trial Version"></asp:Label></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>

                                                        <div class="clearfix"></div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade bannerformmodal" tabindex="-1" role="dialog" aria-labelledby="bannerformmodal" aria-hidden="true" id="bannerformmodal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                            <h4 id="myModalLabel" class="modal-title">About </h4>
                        </div>
                        <div class="modal-body">

                            <embed src="Image/AboutSoftware.pdf" width="100%" height="800" type='application/pdf' />
                        </div>
                        <div class="modal-footer">
                            <button data-dismiss="modal" class="btn btn-default" type="button">Close</button>

                        </div>
                    </div>
                </div>
            </div>


        </div>

    </div>
</asp:Content>


